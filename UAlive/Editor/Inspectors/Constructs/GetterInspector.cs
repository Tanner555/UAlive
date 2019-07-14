using UnityEngine;
using UnityEditor;
using Ludiq;
using Lasm.UAlive;

[assembly: RegisterInspector(typeof(Getter), typeof(GetterInspector))]

namespace Lasm.UAlive
{
    public class GetterInspector : OnGUIBlockInspector
    {
        Accessor getMeta => propertyMeta["get"];
        Accessor setMeta => propertyMeta["set"];
        Accessor scopeMeta => accessor["scope"];
        Accessor useScopeMeta => accessor["useScope"];
        Accessor graphMeta => accessor["graph"];
        Accessor propertyMeta => accessor.parent;
        Accessor fieldMeta => accessor.parent.parent;

        public GetterInspector(Accessor accessor) : base(accessor)
        {

        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(!((Property)propertyMeta.value).isProperty);

            var getRect = position;
            getRect.height = 16;
            getRect.width = 40;

            var useScopeRect = position;
            useScopeRect.x += 40;
            useScopeRect.height = 16;
            useScopeRect.width = 16;

            var y = 0f;

            bool canEditAccessor = ((Property)propertyMeta.value).get && !((Property)propertyMeta.value).set || ((Property)propertyMeta.value).get && ((Property)propertyMeta.value).set;
            bool getAccessible = ((Property)propertyMeta.value).setter.scope == AccessModifier.Public;

            LudiqGUI.Inspector(getMeta, getRect, new GUIContent("Get"));

            EditorGUI.BeginDisabledGroup(!canEditAccessor);
            EditorGUI.BeginDisabledGroup(!getAccessible);
            LudiqGUI.Inspector(scopeMeta, new Rect(useScopeRect.x + 18, useScopeRect.y, (position.width * 0.65f) - 18, 16), GUIContent.none);
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!canEditAccessor || !((Property)propertyMeta.value).isProperty);
            if (GUI.Button(new Rect(useScopeRect.x + position.width * 0.65f + 4, useScopeRect.y, position.width * 0.35f - 4, 16), "Edit"))
            {
                GraphWindow.OpenActive(GraphReference.New((Getter)accessor.value, false));
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndDisabledGroup();

            y += 18;

            height = y;
        }
    }
}