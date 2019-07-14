using UnityEngine;
using UnityEditor;
using Ludiq;
using Ludiq.Bolt;
using Ludiq.OdinSerializer;
using System;
using Lasm.UAlive.Generation;

namespace Lasm.UAlive
{
    [Serializable]
    public class UAliveToolbar : EditorWindow
    {
        public static UAliveToolbar instance;

        [OdinSerialize]
        public bool isLive;
        [OdinSerialize]
        public bool wasLive;

        [MenuItem("Window/Bolt/UAlive/Toolbar")]
        public static void Open()
        {
            if (instance == null) instance = UAliveToolbar.CreateInstance<UAliveToolbar>();
            instance.ShowPopup();
        }

        private void OnGUI()
        {
            if (BoltToolbar.instance != null)
            {
                position = new Rect(BoltToolbar.instance.position.x + 120, BoltToolbar.instance.position.y + 4, 0, 24);
                maxSize = new Vector2(120, 24);
                minSize = new Vector2(120, 24);

                isLive = GUI.Toggle(new Rect(0, 0, 40, 24), isLive, "Live", GUI.skin.button);

                if (isLive != wasLive)
                {
                    var classes = Resources.FindObjectsOfTypeAll<ObjectMacro>().ToListPooled();

                    if (isLive)
                    {
                        classes.Generate(true);
                    }
                    else
                    {
                        classes.Generate(false);
                    }

                    AssetDatabase.Refresh();
                }

                if (GUI.Button(new Rect(position.width + 40, 0, 80, 24), "Generate"))
                {
                    var classes = Resources.FindObjectsOfTypeAll<ObjectMacro>().ToListPooled();

                    if (isLive)
                    {
                        classes.Generate(true);
                    }
                    else
                    {
                        classes.Generate(false);
                    }

                    AssetDatabase.Refresh();
                }

                wasLive = isLive;

                Repaint();
            }
        }
    }
}