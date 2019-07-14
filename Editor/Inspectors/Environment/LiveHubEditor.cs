using Ludiq;
using Lasm.UAlive;
using UnityEngine;

[assembly: RegisterEditor(typeof(LiveHub), typeof(LiveHubEditor))]

namespace Lasm.UAlive
{
    public class LiveHubEditor : OnGUIBlockInspector
    {
        public LiveHubEditor(Accessor accessor) : base(accessor)
        {

        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 200;
        }

        protected override void OnGUIBlock(Rect position, GUIContent label)
        {
            var typesRect = position;
            typesRect.height = accessor["categories"].Inspector().GetCachedHeight(position.width, GUIContent.none, accessor.Editor());

            LudiqGUI.Inspector(accessor["categories"], typesRect, GUIContent.none);

            height = typesRect.height;
        }
    }
}