using UnityEngine;
using Ludiq;

namespace Lasm.UAlive
{
    public class OnGUIBlockInspector : Inspector
    {
        protected virtual bool hasLabel => false;
        protected virtual bool toEdge => true;

        public float height;

        public OnGUIBlockInspector(Accessor accessor) : base(accessor)
        {

        }
         
        protected override float GetHeight(float width, GUIContent label)
        {
            return height;
        }

        protected sealed override void OnGUI(Rect position, GUIContent label)
        {
            var actualPosition = position;
            if (toEdge)
            {
                actualPosition.x -= 13;
                actualPosition.y -= 7;
                actualPosition.width += 17;
            }

            if (hasLabel) BeginBlock(accessor, actualPosition, label);
            else { BeginBlock(accessor, actualPosition, GUIContent.none); }

            if (hasLabel)
            {
                OnGUIBlock(actualPosition, label);
            }
            else
            {
                OnGUIBlock(actualPosition, GUIContent.none);
            }

            if (EndBlock())
            {
                accessor.RecordUndo();
            }
        }

        protected virtual void OnGUIBlock(Rect position, GUIContent label)
        {
        }
    }
}