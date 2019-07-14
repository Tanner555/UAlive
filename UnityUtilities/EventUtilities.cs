using UnityEngine;
using UnityEditor;
namespace Lasm.UnityUtilities
{
    public static class EventUtilities
    {
        private static float lastClickTime = 0f;

#if UNITY_EDITOR
        public static bool isDoubleClickedInEditor(float elapseTime)
        {
            if ((lastClickTime - (float)EditorApplication.timeSinceStartup) * -1 < elapseTime)
            {
                lastClickTime = (float)EditorApplication.timeSinceStartup;
                return true;
            }

            lastClickTime = (float)EditorApplication.timeSinceStartup;
            return false;
        }
#endif

        public static bool isDoubleClicked(float elapseTime)
        {

            if ((lastClickTime - Time.time) * -1 < elapseTime)
            {
                lastClickTime = Time.time;
                return true;
            }

            lastClickTime = Time.time;
            return false;
        }
    }
}
