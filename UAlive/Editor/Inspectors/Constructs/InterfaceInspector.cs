using Ludiq;
using Lasm.UAlive;
using UnityEngine;
using UnityEditor;
using Lasm.UnityEditorUtilities;
using Lasm.UnityUtilities;

[assembly: RegisterInspector(typeof(Interface), typeof(InterfaceInspector))]

namespace Lasm.UAlive
{
    public class InterfaceInspector : OnGUIBlockInspector
    {
        public InterfaceInspector(Accessor accessor) : base(accessor)
        {
            
        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            height = accessor["type"].Inspector().GetCachedHeight(position.width, GUIContent.none, accessor.Inspector()) + 4;

            var dropDownRect = position;
            dropDownRect.x += 15;
            dropDownRect.height = height;
            dropDownRect.width -= 18;
            dropDownRect.y += 8;

            UtilityGUI.Container(dropDownRect, Color.black, 0.6f.Grey(), 1, 2,(container) =>
            {
                LudiqGUI.Inspector(accessor["type"], container, GUIContent.none);
            });
        }
    }
}