using UnityEngine;
using UnityEditor;
using Ludiq;
using Lasm.UAlive;

[assembly: RegisterInspector(typeof(Struct), typeof(StructInspector))]

namespace Lasm.UAlive
{
    public class StructInspector : Inspector
    {
        Rect lastPosition;

        public StructInspector(Accessor accessor) : base(accessor)
        {

        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 74;
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            BeginBlock(accessor, position);

            lastPosition = position;
            lastPosition.y += 24;
            lastPosition.height = 20;

            var nameLabel = lastPosition;
            nameLabel.width = 40;

            var nameField = lastPosition;
            nameField.width -= 40;
            nameField.x += 40;

            EditorGUI.LabelField(nameLabel, new GUIContent("Name"));
            accessor["name"].value = EditorGUI.TextField(nameField, (string)accessor["name"].value);

            lastPosition.y += 24;

            var typeLabel = lastPosition;
            typeLabel.width = 40;

            var typeField = lastPosition;
            typeField.width -= 40;
            typeField.x += 40;

            EditorGUI.LabelField(typeLabel, new GUIContent("Type"));
            LudiqGUI.Inspector(accessor["type"], typeField, GUIContent.none);
            
            if (EndBlock())
            {
                accessor.RecordUndo();
            }
        }
    }
}