using UnityEngine;
using UnityEditor;
using Ludiq;
using Lasm.UnityEditorUtilities;

namespace Lasm.UAlive
{
    public abstract class TypeMacroInspector : OnGUIBlockInspector
    {
        public Rect lastPosition;
        Accessor name { get { return accessor["name"]; } }
        protected Accessor scope => accessor["scope"];
        protected Accessor @namespace => accessor["namespace"];
        ObjectMacro macro => accessor.value as ObjectMacro;
        
        public TypeMacroInspector(Accessor accessor) : base(accessor)
        {
            
        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            var warningRect = CompilerWarning(position);

            lastPosition = Header(warningRect);
            
            height = lastPosition.y;   
        }

        private Rect CompilerWarning(Rect position)
        {
            var warningRect = position;
            warningRect.height = 0;

            if (macro.ShouldCompile())
            {
                warningRect.height = 24;

                UnityEditorUtilities.UtilityGUI.Container(warningRect, Color.black, new Color(0.8f, 0.1f, 0.1f), 1, 4, (contents) =>
                {
                    GUI.Label(contents, new GUIContent("  Scripts are out of date.", UAliveResources.warning), new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter  });
                });
            }

            height += 24;

            return warningRect;
        }

        private Rect Header(Rect position)
        {
            var borderRect = position;
            borderRect.y += position.height;
            borderRect.height = 60;
            height += 60;

            UtilityGUI.Container(borderRect, Color.black, new Color(0.35f, 0.35f, 0.35f), 1, 10, (container) =>
            {
                var nameLabelRect = new Rect(container.x + 64, container.y, 80, 16);

                EditorGUI.LabelField(nameLabelRect, "Name", new GUIStyle(EditorStyles.boldLabel) { normal = new GUIStyleState() { textColor = Color.white } });

                var nameTextRect = new Rect(nameLabelRect.x + nameLabelRect.width + 6, container.y, container.width - nameLabelRect.width - 70, 16);

                name.value = EditorGUI.TextField(nameTextRect, GUIContent.none, (string)name.value);

                var categoryLabelRect = new Rect(container.x + 64, nameLabelRect.y + nameLabelRect.height + 6, 80, 16);

                EditorGUI.LabelField(categoryLabelRect, "Namespace", new GUIStyle(EditorStyles.boldLabel) { normal = new GUIStyleState() { textColor = Color.white } });

                var categoryTextRect = new Rect(categoryLabelRect.x + categoryLabelRect.width + 6, categoryLabelRect.y, container.width - categoryLabelRect.width - 70, 16);

                @namespace.value = EditorGUI.TextField(categoryTextRect, GUIContent.none, (string)@namespace.value);
            });

            return new Rect(position.x, borderRect.y + borderRect.height, position.width, 60);
        }
    }
}