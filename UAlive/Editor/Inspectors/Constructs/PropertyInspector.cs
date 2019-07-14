using UnityEngine;
using Ludiq;
using Lasm.UAlive;

[assembly: RegisterInspector(typeof(Property), typeof(PropertyInspector))]

namespace Lasm.UAlive
{
    public class PropertyInspector : OnGUIBlockInspector
    {
        Accessor getter => accessor["getter"];
        Accessor get => accessor["get"];
        bool getVal => (bool)get.value;
        Accessor setter => accessor["setter"];
        Accessor set => accessor["set"];
        bool setVal => (bool)set.value;
        Accessor field => accessor.parent;

        public PropertyInspector(Accessor accessor) : base(accessor)
        {

        }
        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            var y = 0;

            var lastPosition = position;
            lastPosition.height = 20;
            y += 16;

            LudiqGUI.Inspector(getter, lastPosition, GUIContent.none);
            lastPosition.y += 20;
            y += 20;
            if (((Getter)getter.value).useScope) ((Setter)setter.value).useScope = !((Getter)getter.value).useScope;

            LudiqGUI.Inspector(setter, lastPosition, GUIContent.none);
            lastPosition.y += 20;
            y += 20;
            if (((Setter)setter.value).useScope) ((Getter)getter.value).useScope = !((Setter)setter.value).useScope;

            height = y;
        }
    }
}