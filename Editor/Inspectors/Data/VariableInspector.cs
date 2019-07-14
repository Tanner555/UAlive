using UnityEngine;
using UnityEditor;
using Ludiq;
using Lasm.UAlive;
using Lasm.UnityEditorUtilities;
using Lasm.UnityUtilities;
using System.Collections.Generic;
using Lasm.Reflection;

[assembly: RegisterInspector(typeof(Variable), typeof(VariableInspector))]

namespace Lasm.UAlive
{
    public class VariableInspector : OnGUIBlockInspector
    {
        Rect lastPosition;
        Accessor nameMeta => accessor["name"];
        Accessor typeMeta => accessor["type"];
        Accessor scopeMeta => accessor["scope"];
        Accessor modifierMeta => accessor["fieldModifier"];
        Accessor isPropertyMeta => propertyMeta["isProperty"];
        Accessor propertyMeta => accessor["property"];
        Accessor isOpenMeta => accessor["isOpen"];
        private const float padding = 4;

        public VariableInspector(Accessor accessor) : base(accessor)
        {
        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            y = 0f;

            var containerRect = position;
            containerRect.x += 15;
            containerRect.height = height;
            containerRect.width -= 18;
            containerRect.y += 4;

            var backgroundColor = GUI.color;

            UtilityGUI.Container(containerRect, Color.black, 0.8f.Grey(), 1, 0, (container) =>
            {
                Header(container);
                OpenContents(position, containerRect);
            });


            height = y;
        }

        private void Header(Rect container)
        {
            var indexGreaterThanOne = (accessor.parent.IndexOf(accessor.value) > 0);

            UtilityGUI.Container(new Rect(container.x, container.y + (indexGreaterThanOne ? 1 : 0), container.width, 26), Color.black, 0.6f.Grey(), 1, 5, (_container) =>
            {
                var headerRect = new Rect(container.x, container.y + (indexGreaterThanOne ? 1 : 0), container.width, 26);
                Texture2D arrow = (bool)isOpenMeta.value ? UAliveResources.arrowDownDark : UAliveResources.arrowRightDark;
                Graphics.DrawTexture(new Rect(container.x + container.width - 20, container.y + 9, 10, 10), arrow);
                nameMeta.value = EditorGUI.TextField(new Rect(container.x + 32, container.y + (indexGreaterThanOne ? 5 : 4), container.width - 64, 18), (string)nameMeta.value);
                var type = (typeMeta.value as TypeValue).type;
                var _icon = type.FindIcon();

                if (_icon != null) Graphics.DrawTexture(new Rect(container.x + 8, container.y + 5, 16, 16), _icon);

                if (_container.Contains(e.mousePosition) && e.type == EventType.MouseDown && e.button == 0)
                {
                    isOpenMeta.value = !(bool)isOpenMeta.value;
                }

                // We are using a control that is invisible, so we cant prevent that area of the list from trying to be moved.
                GUI.Button(headerRect, GUIContent.none, GUIStyle.none);
                y += 20;
            });
        }

        private void OpenContents(Rect position, Rect container)
        {
            if ((bool)isOpenMeta.value)
            {
                var typeHeight = LudiqGUI.GetInspectorHeight(typeMeta.Inspector(), typeMeta["type"], position.width, GUIContent.none);
                var valueHeight = LudiqGUI.GetInspectorHeight(typeMeta.Inspector(), typeMeta["value"], position.width, GUIContent.none);
                var typeLabel = new Rect(container.x + 5, container.y + y + 10, 60, 16);
                var typeRect = new Rect(container.x + 50, container.y + typeLabel.height + 16, container.width - 60, typeHeight);
                var defaultRect = new Rect(container.x + 50, typeRect.y + typeRect.height + 4, container.width - 60, typeHeight);
                var valueRect = new Rect(container.x + 10, typeRect.y + typeRect.height + defaultRect.height + 8, container.width - 20, valueHeight + 8);
                EditorGUI.LabelField(typeLabel, new GUIContent("Type"));
                LudiqGUI.Inspector(typeMeta["type"], typeRect, GUIContent.none);

                var addedHeight = 0;

                if (!((System.Type)typeMeta["type"].value).InheritsType(typeof(UnityEngine.Object)))
                {
                    addedHeight += 16;

                    LudiqGUI.Inspector(typeMeta["hasDefault"], defaultRect, new GUIContent("Default"));

                    if ((bool)typeMeta["hasDefault"].value)
                    {
                        UtilityGUI.Container(valueRect, Color.black, 0.7f.Grey(), 1, 4, (valueContainer) =>
                        {
                            if (typeMeta["value"].value != null || ((System.Type)typeMeta["type"].value).InheritsType(typeof(UnityEngine.Object)))
                            {
                                LudiqGUI.Inspector(typeMeta["value"], valueContainer, GUIContent.none);

                                if ((System.Type)typeMeta["type"].value == typeof(bool))
                                {
                                    var boolLabel = valueContainer;
                                    boolLabel.x += 18;
                                    GUI.Label(boolLabel, (nameMeta.value as string).Prettify());
                                }
                            }
                        });

                        addedHeight += (int)valueRect.height + 10;
                    }
                }

                var optionsRect = Options(typeLabel, typeRect, addedHeight + defaultRect.height);
                var propertiesRect = optionsRect;
                propertiesRect.width = typeLabel.width + typeRect.width;
                propertiesRect.x -= 20;
                propertiesRect.y += 86;
                propertiesRect.height = 56;
                var propertyHeight = propertyMeta.Inspector().GetCachedHeight(lastPosition.width, GUIContent.none, accessor.Inspector()) + addedHeight;

                var prop = ((Property)propertyMeta.value);
                var propName = string.Empty;

                UAliveGUI.NestedSection(propertiesRect, e, "Property", UAliveResources.properties, addedHeight, (propertiesContainer) =>
                {
                    LudiqGUI.Inspector(propertyMeta, new Rect(propertiesContainer.x, propertiesContainer.y, propertiesContainer.width - 40, propertyHeight));
                }, isPropertyMeta);

                addedHeight += 30;

                y += typeHeight + defaultRect.height + optionsRect.height + propertiesRect.height + addedHeight + 4;
            }
        }

        public Rect Options(Rect typeLabel, Rect typeDropDown, float heightModifier)
        {
            var fullRect = typeLabel;
            fullRect.height = 78;
            fullRect.x += 15;
            fullRect.width += typeDropDown.width - 40;
            fullRect.y += heightModifier + 8;

            UtilityGUI.Container(fullRect, Color.black, 0.7f.Grey(), 1, 4, (readContainer) =>
            {
                var scopeRect = readContainer;
                scopeRect.height = 22;

                var modifierRect = scopeRect;
                modifierRect.y += 24;

                var readOnlyRect = modifierRect;
                readOnlyRect.y += 24;

                UtilityGUI.Container(scopeRect, Color.black, 0.8f.Grey(), 1, 2, (scopeContainer) =>
                {
                    GUI.Label(new Rect(scopeContainer.x, scopeContainer.y + 1, 70, scopeContainer.height), "Scope");
                    LudiqGUI.Inspector(scopeMeta, new Rect(scopeContainer.x + 70, scopeContainer.y + 1, scopeContainer.width - 70, scopeContainer.height), GUIContent.none);
                });

                UtilityGUI.Container(modifierRect, Color.black, 0.8f.Grey(), 1, 2, (modifierContainer) =>
                {
                    GUI.Label(new Rect(modifierContainer.x, modifierContainer.y + 1, 70, modifierContainer.height), "Modifier");
                    LudiqGUI.Inspector(modifierMeta, new Rect(modifierContainer.x + 70, modifierContainer.y + 1, modifierContainer.width - 70, modifierContainer.height), GUIContent.none);
                });

                UtilityGUI.Container(readOnlyRect, Color.black, 0.8f.Grey(), 1, 2, (readOnlyContainer) =>
                {
                    GUI.Label(new Rect(readOnlyContainer.x, readOnlyContainer.y + 1, 70, readOnlyContainer.height), "Read Only");
                    LudiqGUI.Inspector(accessor["isReadOnly"], new Rect(readOnlyContainer.x + 70, readOnlyContainer.y + 1, readOnlyContainer.width - 70, readOnlyContainer.height), GUIContent.none);
                });
            });

            return fullRect;
        }
    }
}