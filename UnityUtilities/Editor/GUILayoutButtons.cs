using System;
using UnityEngine;
using UnityEditor;

namespace Lasm.UnityEditorUtilities
{
    public partial class UtilityGUILayout
    {
        /// <summary>
        /// Draws an button that uses an image instead of GUIContent or text. Button can simultaneously be used as a toggle.
        /// </summary>
        /// <param name="isActiveSelection"></param>
        /// <param name="size"></param>
        /// <param name="normal"></param>
        /// <param name="active"></param>
        /// <param name="pressed"></param>
        /// <returns></returns>
        public static bool ImageButton(bool isActiveSelection, Vector2 size, Texture2D normal, Texture2D active, Texture2D pressed)
        {
            var style = new GUIStyle();

            if (!isActiveSelection)
            {
                style.focused.background = active;
                style.active.background = pressed;
                style.hover.background = active;
                style.normal.background = normal;

                if (GUILayout.Button(GUIContent.none, style, GUILayout.Height(size.y), GUILayout.Width(size.x)))
                {
                    return true;
                }

            }
            else
            {
                style.normal.background = active;
                GUILayout.Box(GUIContent.none, style, GUILayout.Height(size.y), GUILayout.Width(size.x));
            }

            return false;
        }

        /// <summary>
        /// Draws an button that uses an image instead of GUIContent or text. Button can simultaneously be used as a toggle.
        /// </summary>
        /// <param name="isActiveSelection"></param>
        /// <param name="normal"></param>
        /// <param name="active"></param>
        /// <param name="pressed"></param>
        /// <returns></returns>
        public static bool ImageButton(bool isActiveSelection, Texture2D normal, Texture2D active, Texture2D pressed)
        {
            var style = new GUIStyle();

            if (!isActiveSelection)
            {
                style.focused.background = active;
                style.active.background = pressed;
                style.hover.background = active;
                style.normal.background = normal;

                if (GUILayout.Button(GUIContent.none, style))
                {
                    return true;
                }

            }
            else
            {
                style.normal.background = active;
                GUILayout.Box(GUIContent.none, style);
            }

            return false;
        }

        /// <summary>
        /// Draws a bordered rect that can be hovered and pressed as a button.
        /// </summary>
        /// <param name="isPressed"></param>
        /// <param name="fullRect"></param>
        /// <param name="borderThickness"></param>
        /// <param name="background"></param>
        /// <param name="border"></param>
        /// <param name="highlight"></param>
        /// <param name="pressed"></param>
        /// <returns></returns>
        public static bool BorderedRectButton(SelectionStyleContent content, ref bool isPressed)
        {
            bool wasPressed = false;

            var image = content.image;
            var text = content.text;
            var tooltip = content.tooltip;

            Color currentColor = content.background;

            GUILayout.Box(GUIContent.none, GUIStyle.none, GUILayout.Height(content.height));

            var fullRect = GUILayoutUtility.GetLastRect();

            if (fullRect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    if (Event.current.button == 0)
                    {
                        isPressed = true;
                    }
                }
                else
                {
                    if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
                    {
                        isPressed = false;
                        wasPressed = true;
                    }
                }

                if (isPressed)
                {
                    currentColor = content.pressed;
                }
                else
                {
                    currentColor = content.active;
                }
            }

            UtilityGUI.BorderRect(fullRect, content.borderThickness, content.pressed, content.border, UtilityGUI.BorderDrawPlacement.Outside);

            return wasPressed;
        }

        public static bool BorderedRectButton(SelectionStyleContent content, ref bool isActiveSelection, ref bool isPressed)
        {
            bool wasPressed = false;
            var image = content.image;
            var text = content.text;
            var tooltip = content.tooltip;

            Color currentColor = content.background;
            
            GUILayout.Box(GUIContent.none, GUIStyle.none, GUILayout.Height(content.height));

            var fullRect = GUILayoutUtility.GetLastRect();

            if (fullRect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                {
                        isPressed = true;
                }
                else
                {
                    if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
                    {
                        wasPressed = true;
                        isPressed = false;
                    }
                }

                if (isPressed)
                {
                    currentColor = content.pressed;
                } else
                {
                    currentColor = content.active;
                }
            }
            else
            {
                if (isActiveSelection)
                {
                    currentColor = content.active;
                }
            }

            UtilityGUI.BorderRect(fullRect, content.borderThickness, currentColor, content.border, UtilityGUI.BorderDrawPlacement.Outside);

            return wasPressed;
        }
    }
}
