using UnityEngine;
using UnityEditor;
using Ludiq;
using Lasm.UAlive;

[assembly: RegisterInspector(typeof(Enum), typeof(Lasm.UAlive.EnumInspector))]

namespace Lasm.UAlive
{
    public class EnumInspector : Inspector
    {
        Rect lastPosition;

        public EnumInspector(Accessor accessor) : base(accessor)
        {

        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 74;
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            BeginBlock(accessor, position);

            Rect nameLabel = new Rect();
            Rect nameField = new Rect();
            Rect indexLabel = new Rect();
            Rect indexField = new Rect();

            CreateRects(position, out nameLabel, out nameField, out indexLabel, out indexField);

            DrawControls(nameLabel, nameField, indexLabel, indexField);

            if (EndBlock())
            {
                accessor.RecordUndo();
            }
        }

        private void CreateRects(Rect position, out Rect nameLabel, out Rect nameField, out Rect indexLabel, out Rect indexField)
        {
            lastPosition = position;
            lastPosition.y += 24;
            lastPosition.height = 20;

            nameLabel = lastPosition;
            nameLabel.width = 40;

            nameField = lastPosition;
            nameField.width -= 40;
            nameField.x += 40;

            lastPosition.y += 24;

            indexLabel = lastPosition;
            indexLabel.width = 40;

            indexField = lastPosition;
            indexField.width = 40;
            indexField.x += 40;
        }

        private void DrawControls(Rect nameLabel, Rect nameField, Rect indexLabel, Rect indexField)
        {
            EditorGUI.LabelField(nameLabel, new GUIContent("Name"));
            accessor["name"].value = EditorGUI.TextField(nameField, (string)accessor["name"].value);

            EditorGUI.LabelField(indexLabel, new GUIContent("Index"));
            accessor["index"].value = EditorGUI.IntField(indexField, (int)accessor["index"].value);
        }
    }
}