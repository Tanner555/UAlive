using UnityEngine;
using UnityEditor;
using Ludiq;
using Lasm.UAlive;

[assembly: RegisterInspector(typeof(Setter), typeof(SetterInspector))]

namespace Lasm.UAlive
{
    public class SetterInspector : OnGUIBlockInspector
    {
        Accessor getMeta => propertyMeta["get"];
        Accessor setMeta => propertyMeta["set"];
        Accessor scopeMeta => accessor["scope"];
        Accessor useScopeMeta => accessor["useScope"];
        Accessor graphMeta => accessor["graph"];
        Accessor propertyMeta => accessor.parent;
        Accessor fieldMeta => accessor.parent.parent;

        public SetterInspector(Accessor accessor) : base(accessor)
        {

        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return height;
        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(!((Property)propertyMeta.value).isProperty);

            var setRect = position;
            setRect.height = 16;
            setRect.width = 40;

            var useScopeRect = position;
            useScopeRect.x += 40;
            useScopeRect.height = 16;
            useScopeRect.width = 16;

            var y = 0f;

            bool canEditAccessor = ((Property)propertyMeta.value).set && !((Property)propertyMeta.value).get || ((Property)propertyMeta.value).set && ((Property)propertyMeta.value).get;
            bool setAccessible = ((Property)propertyMeta.value).getter.scope == AccessModifier.Public;
            LudiqGUI.Inspector(setMeta, setRect, new GUIContent("Set"));

            EditorGUI.BeginDisabledGroup(!canEditAccessor);
            EditorGUI.BeginDisabledGroup(!setAccessible);
            LudiqGUI.Inspector(scopeMeta, new Rect(useScopeRect.x + 18, useScopeRect.y, (position.width * 0.65f) - 18, 16), GUIContent.none);
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!canEditAccessor);
            if (GUI.Button(new Rect(useScopeRect.x + position.width * 0.65f + 4, useScopeRect.y, position.width * 0.35f - 4, 16), "Edit"))
            {
                GraphWindow.OpenActive(GraphReference.New((Setter)accessor.value, false));
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndDisabledGroup();
            
            y += 18;

            height = y;
        }
    }
}