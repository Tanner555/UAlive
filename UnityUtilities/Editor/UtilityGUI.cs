using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

namespace Lasm.UnityEditorUtilities
{
    public partial class UtilityGUI 
    {
        /// <summary>
        /// Draws a rect that has a border.
        /// </summary>
        /// <param name="backgroundRect">The rect we will draw.</param>
        /// <param name="borderThickness">The border size in pixels.</param>
        /// <param name="background">The color of the rect that is the background. This is drawn second.</param>
        /// <param name="border">The color of the rect that is the border. This is drawn first.</param>
        /// <param name="drawPlacement">The placement of the border. Inside will draw the border starting at give rects initial size and inward. Outside draws outward. Centered draws equal in and outside of the rect.</param>
        /// <returns></returns>
        public static Rect BorderRect(Rect backgroundRect, float borderThickness, Color background, Color border, BorderDrawPlacement drawPlacement)
        {
            Rect borderRect = new Rect();
            Rect newBackgroundRect = new Rect();

            switch (drawPlacement)
            {
                case BorderDrawPlacement.Inside:
                    borderRect = backgroundRect;

                    newBackgroundRect = new Rect(
                        backgroundRect.x + borderThickness,
                        backgroundRect.y + borderThickness,
                        backgroundRect.width - (borderThickness * 2),
                        backgroundRect.height - (borderThickness * 2)
                        );

                    break;
                case BorderDrawPlacement.Outside:
                    borderRect = new Rect(
                    backgroundRect.x - borderThickness,
                    backgroundRect.y - borderThickness,
                    backgroundRect.width + (borderThickness * 2),
                    backgroundRect.height + (borderThickness * 2)
                    );

                    newBackgroundRect = backgroundRect;
                    break;
                case BorderDrawPlacement.Centered:
                    borderRect = new Rect(
                    backgroundRect.x - (borderThickness / 2),
                    backgroundRect.y - (borderThickness / 2),
                    backgroundRect.width + (borderThickness / 2),
                    backgroundRect.height + (borderThickness / 2)
                    );

                    newBackgroundRect = new Rect(
                    backgroundRect.x + (borderThickness / 2),
                    backgroundRect.y + (borderThickness / 2),
                    backgroundRect.width - borderThickness,
                    backgroundRect.height - borderThickness
                    );
                    break;
            }


            EditorGUI.DrawRect(borderRect, border);

            EditorGUI.DrawRect(newBackgroundRect, background);

            return borderRect;

        }


        public static void Container(Rect position, int padding, Action<Rect> contents)
        {
            var paddedRect = new Rect(position.x + padding, position.y + padding, position.width - (padding * 2), position.height - (padding * 2));
            contents(paddedRect);
        }

        public static void Container(Rect position, Color border, Color background, int borderThickness, int padding, Action<Rect> contents)
        {
            var paddedRect = new Rect(position.x + padding, position.y + padding, position.width - (padding * 2), position.height - (padding * 2));

            BorderRect(position, borderThickness, background, border, BorderDrawPlacement.Inside);

            contents(paddedRect);
        }

        /// <summary>
        /// Where the placement of a border goes.
        /// </summary>
        public enum BorderDrawPlacement
        {
            Inside,
            Outside,
            Centered
        }
    }
}