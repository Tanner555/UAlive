using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using Lasm.UnityUtilities;

namespace Lasm.UnityEditorUtilities
{
    public partial class UtilityGUI
    {
        /// <summary>
        /// Draws an button that uses an image instead of GUIContent or text. Button can simultaneously be used as a toggle.
        /// </summary>
        /// <param name="isBeingPressed"></param>
        /// <param name="isActiveSelection"></param>
        /// <param name="position"></param>
        /// <param name="normal"></param>
        /// <param name="active"></param>
        /// <param name="pressed"></param>
        /// <returns></returns>
        public static bool ImageButton(ref bool isBeingPressed, bool isActiveSelection, Rect position, Texture2D normal, Texture2D active, Texture2D pressed)
        {
            Texture2D current = normal;
            bool wasPressed = false;
            bool justReleased = false;

            if (Event.current.type == EventType.MouseDown)
            {
                isBeingPressed = true;
            }

            if (Event.current.type == EventType.MouseUp)
            {
                if (isBeingPressed == true)
                {
                    justReleased = true;
                }

                isBeingPressed = false;
            }

            if (isActiveSelection)
            {
                current = active;
                isBeingPressed = false;
            }
            else
            {
                if (position.Contains(Event.current.mousePosition))
                {
                    if (isBeingPressed)
                    {
                        current = pressed;
                    }
                    else
                    {
                        if (justReleased)
                        {
                            current = normal;
                            wasPressed = true;
                            isBeingPressed = false;
                        } else
                        {
                            current = active;
                            isBeingPressed = false;
                        }
                    }
                }
                else
                {
                    isBeingPressed = false;
                    current = normal;
                }
            }

            GUI.DrawTexture(position, current);

            if (wasPressed)
            {
                return true;
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
        public static bool BorderedRectButton(ref bool isPressed, Rect fullRect, UtilityGUILayout.SelectionStyleContent content)
        {
            bool wasPressed = false;

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

            if (fullRect.Contains(Event.current.mousePosition))
            {
                if (isPressed)
                {
                    BorderRect(fullRect, content.borderThickness, content.pressed, content.border, BorderDrawPlacement.Outside);
                }
                else
                {
                    BorderRect(fullRect, content.borderThickness, content.active, content.border, BorderDrawPlacement.Outside);
                }
            }
            else
            {
                BorderRect(fullRect, content.borderThickness, content.background, content.border, BorderDrawPlacement.Outside);
            }

            return wasPressed;
        }
    }
}