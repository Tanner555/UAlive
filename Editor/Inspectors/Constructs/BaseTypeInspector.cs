using Ludiq;
using Lasm.UAlive;
using UnityEngine;
using UnityEditor;
using Lasm.UnityUtilities;
using Lasm.UnityEditorUtilities;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: RegisterInspector(typeof(BaseType), typeof(BaseTypeInspector))]

namespace Lasm.UAlive
{
    public class BaseTypeInspector : OnGUIBlockInspector
    {
        private ObjectMacro activeSelection => Selection.activeObject as ObjectMacro;

        public BaseTypeInspector(Accessor accessor) : base(accessor)
        {

        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            var baseTypeRect = position;

            UtilityGUI.Container(baseTypeRect, Color.black, 0.6f.Grey(), 1, 4, (container) =>
            {
                LudiqGUI.Inspector(accessor["type"], container, GUIContent.none);
            });

            height = accessor["type"].Inspector().GetCachedHeight(position.width, GUIContent.none, accessor.Inspector());
        }
    }
}