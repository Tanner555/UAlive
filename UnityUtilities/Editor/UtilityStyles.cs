using System;
using UnityEngine;
using UnityEditor;
using Lasm.UnityUtilities;

namespace Lasm.UnityEditorUtilities
{
    public static class UtilityStyles
    {
        public static GUIStyle DarkTextField()
        {
            GUIStyle style = new GUIStyle(EditorStyles.textField);

            Color whitesmoke = 0.9f.Grey();
            Color lightBlack = 0.3f.Grey();

            style.normal.textColor = whitesmoke;
            style.active.textColor = whitesmoke;
            style.focused.textColor = whitesmoke;
            
            return style;
        }
    }


}
