using Ludiq;
using Lasm.UAlive;
using UnityEngine;
using Lasm.UnityUtilities;
using Lasm.UnityEditorUtilities;

[assembly: RegisterEditor(typeof(Contract), typeof(ContractInspector))]

namespace Lasm.UAlive
{
    public class ContractInspector : OnGUIBlockInspector
    {
        public Vector2 scrollPosition;

        public ContractInspector(Accessor accessor) : base(accessor)
        {

        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            var contractRect = position;
        }
    }
}