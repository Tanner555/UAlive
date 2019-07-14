using Ludiq;
using Lasm.UAlive;
using Lasm.Reflection;
using UnityEngine;
using UnityEditor;

[assembly: RegisterInspector(typeof(Yieldable), typeof(YieldableInspector))]

namespace Lasm.UAlive
{
    public class YieldableInspector : OnGUIBlockInspector
    {
        private Accessor yieldable => accessor;
        private Accessor canYield => yieldable["canYield"];
        private Accessor @yield => yieldable["yield"];
        float _height = 0f;

        private bool canShowYield;

        public YieldableInspector(Accessor accessor) : base(accessor)
        {
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return _height;
        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            var toggleRect = position;
            toggleRect.height = 16;

            var returnUnit = (accessor.parent.value as ReturnUnit);

            var entry = returnUnit?.entry as MethodInputUnit;

            if (entry?.declaration?.returns != null)
            {
                canYield.value = entry.declaration.returns.IsIEnumerator();
                if ((bool)canYield.value) LudiqGUI.Inspector(@yield, toggleRect, new GUIContent("Yield"));
            }

            _height = (yieldable.value != null ? (((bool)canYield.value) ? 20f : 0f) : 0f);
        }
    }
}