using Lasm.UAlive;
using Lasm.UnityEditorUtilities;
using Lasm.UnityUtilities;
using Ludiq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[assembly: RegisterEditor(typeof(ObjectMacro), typeof(ObjectMacroInspector))]

namespace Lasm.UAlive
{
    public class ObjectMacroInspector : TypeMacroInspector
    {
        Accessor methods => accessor["methods"];
        Accessor classModifier => accessor["classModifier"];
        Accessor kind => accessor["kind"];
        Accessor variables => accessor["variables"];
        Accessor variablesOpen => accessor["variablesOpen"];
        protected Accessor baseType => accessor["baseType"];
        protected Accessor interfaces => accessor["interfaces"];
        ObjectMacro macro => accessor.value as ObjectMacro;
        public List<string> usingStatements = new List<string>();
        private int lastVariablesCount;
        private int lastDynamicVariablesCount;
        private int lastMethodsCount;
        private int lastDynamicMethodsCount;

        public ObjectMacroInspector(Accessor accessor) : base(accessor)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return height;
        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            base.OnGUIBlock(position, label);

            UAliveResources.CacheTextures();

            // Methods and Fields are anonymous within a Dictionary. 
            // In order for them to be aware they are owned by this class, 
            // this class must assign them a reference to itself
            OnMethodCountChanged();
            OnVariableCountChanged();

            // We must clear the variable.
            // This inherits from OnGUIBlock. 
            // Set the height to the Y at the end. 
            // Get Height auto returns that variable.
            y = 0f;

            var accessibilityRect = Accessibility(lastPosition);

            var baseRect = BaseType(position, accessibilityRect);

            var interfacesRect = UAliveGUI.DrawSection
            (
                baseRect, 
                accessor["interfaces"], 
                accessor, 
                e, 
                "Interfaces",
                UAliveResources.contract, 
                52, 
                true, 
                interfaces.Count, 
                accessor["interfacesOpen"]
            );

            var variablesRect = UAliveGUI.DrawSection
            (
                interfacesRect,
                accessor["variables"],
                accessor,
                e,
                "Variables",
                UAliveResources.variable,
                52,
                true,
                variables.Count,
                accessor["variablesOpen"],
                isEditor: false,
                new List<Rect>() { new Rect(interfacesRect.x + interfacesRect.width - 120, interfacesRect.y + interfacesRect.height + 14, 80, 16) }
            );

            VariableOverrideButton(new Rect(variablesRect.x + variablesRect.width - 120, variablesRect.y + 14, 80, 16));

            lastPosition.y += variablesRect.height;
            y += variablesRect.height;

            ForceAllPrivate();

            var methodsOpen = (bool)accessor["methodsOpen"].value;

            var methodsHeight = methodsOpen ? (int)accessor["methods"].Inspector<MethodsInspector>().GetCachedHeight(position.width, GUIContent.none, accessor.Editor<ObjectMacroInspector>()) : 52;
            if (methodsOpen && (int)accessor["methods"].Count == 0) methodsHeight = 35;


            var methodsRect = UAliveGUI.DrawSection
            (
                variablesRect,
                accessor["methods"],
                accessor,
                e,
                "Methods",
                UAliveResources.method,
                methodsHeight,
                true,
                methods.Count,
                accessor["methodsOpen"],
                isEditor: true
            );

            height += variablesRect.height + methodsRect.height + interfacesRect.height + baseRect.height + accessibilityRect.height;
        }

        private Rect Accessibility(Rect position)
        {
            // Set the surrounding box around scope and special.
            var accessibilityRect = position;
            accessibilityRect.y -= 1;
            accessibilityRect.height = 82;

            // Draw a rectangle that contains a border and is light grey. Color usage for differentiating sections.
            UtilityGUI.Container(accessibilityRect, Color.black, 0.6f.Grey(), 1, 10, (container) =>
            {
                var scopeFullRect = container;
                scopeFullRect.height = container.height / 2 - 4;

                var kindRect = scopeFullRect;
                kindRect.y = scopeFullRect.y + scopeFullRect.height + 8;

                UtilityGUI.Container(scopeFullRect, Color.black, 0.7f.Grey(), 1, 6, (scopeContainer) =>
                {
                    // Set the rectangle for the scopes label
                    var scopeRectLabel = scopeContainer;
                    scopeRectLabel.width = 60;

                    // Set the rectangle for the scopes control
                    var scopeRect = scopeContainer;
                    scopeRect.x += 60;
                    scopeRect.width = (scopeContainer.width / 2) - 64;

                    // Set the rectangle for the specials label
                    var specialLabelRect = scopeRect;
                    specialLabelRect.x += scopeRect.width + 10;
                    specialLabelRect.width = 60;

                    // Set the rectangle for the specials control
                    var specialRect = scopeRect;
                    specialRect.x += scopeRect.width + specialLabelRect.width + 4;
                    specialRect.width = (accessibilityRect.width / 2) - 80;

                    // Draw the scopes label
                    GUI.Label(scopeRectLabel, new GUIContent("Scope"));
                    Graphics.DrawTexture(new Rect(scopeRectLabel.x + 40, scopeRectLabel.y, 16, 16), UAliveResources.scope);

                    // Draw the scope drop down
                    LudiqGUI.Inspector(scope, scopeRect, GUIContent.none);

                    // Draw the special label
                    GUI.Label(specialLabelRect, new GUIContent("Modifier"));

                    // Draw the special control
                    LudiqGUI.Inspector(classModifier, specialRect, GUIContent.none);
                });

                Kind(kindRect);
            });

            return accessibilityRect;
        }

        private Rect Kind(Rect container)
        {
            Rect output = Rect.zero;

            UtilityGUI.Container(container, Color.black, 0.7f.Grey(), 1, 6, (kindContainer) =>
            {
                // Set the rectangle for the scopes label
                var kindRectLabel = kindContainer;
                kindRectLabel.width = 60;

                // Set the rectangle for the scopes control
                var _kindRect = kindContainer;
                _kindRect.x += 60;
                _kindRect.width = (kindContainer.width / 2) - 64;

                ObjectKind _kind = (ObjectKind)kind.value;

                Texture2D kindIcon = null;

                switch (_kind)
                {
                    case ObjectKind.Class:
                        kindIcon = UAliveResources.@class;
                        break;

                    case ObjectKind.Struct:
                        kindIcon = UAliveResources.@struct;
                        break;

                    case ObjectKind.Interface:
                        kindIcon = UAliveResources.@interface;
                        break;

                    case ObjectKind.Enum:
                        kindIcon = UAliveResources.@enum;
                        break;

                    case ObjectKind.Event:
                        kindIcon = UAliveResources.@event;
                        break;
                }

                GUI.Label(kindRectLabel, new GUIContent("Kind"));
                Graphics.DrawTexture(new Rect(kindRectLabel.x + 40, kindRectLabel.y, 16, 16), kindIcon);

                // Draw the special control
                LudiqGUI.Inspector(kind, _kindRect, GUIContent.none);

                output = _kindRect;
            });

            return output;
        }
        
        private Rect BaseType(Rect full, Rect previous)
        {
            if ((ObjectKind)kind.value == ObjectKind.Class)
            {
                var genericsHeight = GetGenericArgumentsHeight(GenericBaseTypeConstruct.Base, full.width, 14);
                var baseTypeRect = previous;
                baseTypeRect.y += previous.height - 1;
                baseTypeRect.height = genericsHeight + 40 + 9;

                // Draw a rectangle that contains a border and is light grey. Color usage for differentiating sections.
                UtilityGUI.Container(baseTypeRect, Color.black, 0.6f.Grey(), 1, 10, (container) =>
                {
                    UtilityGUI.Container(container, Color.black, 0.8f.Grey(), 1, 4, (_container) =>
                    {
                        var icon = UAliveIcons.SetWhenNull("Type", UAliveIcons.IconFromPathLudiq("System.Type", UAliveIcons.IconSize.Large));

                        UtilityGUI.BorderRect(new Rect(container.x, container.y, container.width, 24), 1, 0.3f.Grey(), Color.black, UtilityGUI.BorderDrawPlacement.Inside);
                        Graphics.DrawTexture(new Rect(container.x + 5, container.y + 4, 16, 16), icon);
                        EditorGUI.LabelField(new Rect(_container.x + 20, _container.y, _container.width - 26, 16), "Base Type", new GUIStyle(EditorStyles.boldLabel) { normal = new GUIStyleState() { textColor = Color.white } });
                        LudiqGUI.Inspector(baseType, new Rect(_container.x + 14, _container.y + 32, _container.width - 20, genericsHeight - 6), GUIContent.none);
                    });
                });

                return baseTypeRect;
            }

            return previous;
        }

        private void ForceAllPrivate()
        {
            bool forcePrivate = false;

            if ((AccessModifier)scope.value == AccessModifier.Private)
            {
                forcePrivate = true;
            }

            foreach (Variable prop in variables)
            {
                if (forcePrivate) prop.scope = AccessModifier.Private;
            }
        }

        private void OnMethodCountChanged()
        {
            var methods = (Methods)this.methods.value;

            if (lastMethodsCount != methods.Count)
            {
                RecalibrateMethods(methods);
            }

            lastMethodsCount = methods.Count;
        }

        private void RecalibrateVariables(List<Variable> variables)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                variables[i].root = (ObjectMacro)accessor.value;
            }
        }

        private void RecalibrateMethods(Methods methods)
        {
            var objectAdded = false;
            for (int i = 0; i < methods.Count; i++)
            {
                if (methods[i] == null)
                {
                    methods[i] = ScriptableObject.CreateInstance<Method>();
                    if (!AssetDatabase.Contains(methods[i])) AssetDatabase.AddObjectToAsset(methods[i], (ObjectMacro)accessor.value);
                    AssetDatabase.SaveAssets();
                    objectAdded = true;
                }

                methods[i].owner = (ObjectMacro)accessor.value;
            }

            if (objectAdded) AssetDatabase.Refresh();
        }

        private void OnVariableCountChanged()
        {
            var variables = (List<Variable>)this.variables.value;

            if (lastVariablesCount != variables.Count)
            {
                RecalibrateVariables(variables);
            }

            lastVariablesCount = variables.Count;
        }

        private float GetGenericArgumentsHeight(GenericBaseTypeConstruct typeConstruct, float width, int padding)
        {

            if (typeConstruct == GenericBaseTypeConstruct.Base)
            {
                var _height = baseType.Inspector().GetCachedHeight(width, GUIContent.none, accessor.Inspector()) + padding;
                return _height;
            }

            if (typeConstruct == GenericBaseTypeConstruct.Interface)
            {
                var _height = 0f;

                for (int i = 0; i < interfaces.Count; i++)
                {
                    _height += interfaces[i].Inspector().GetCachedHeight(width, GUIContent.none, accessor.Inspector()) + padding;
                }

                return _height;
            }

            return 0f;
        }

        private void VariableOverrideButton(Rect position)
        {
            EditorGUI.BeginDisabledGroup(true);

            if (GUI.Button(position, new GUIContent("+ Override")))
            {

            }

            EditorGUI.EndDisabledGroup();
        }

        private void FakeButton(Rect position)
        {
            GUI.Button(position, GUIContent.none, GUIStyle.none);
        }

        public override float GetAdaptiveWidth()
        {
            return 500;
        }

        private enum RecalibrationDirection
        {
            Less,
            Equal,
            Greater
        }

        private enum GenericBaseTypeConstruct
        {
            Base,
            Interface
        }
    }
}