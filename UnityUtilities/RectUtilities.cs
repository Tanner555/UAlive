using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Lasm.UnityUtilities
{ 
    public static class RectUtilities
    {
        /// <summary>
        /// Returns a split rect in a row at index.
        /// </summary>
        /// <param name="rect">The position and size of the original rect.</param>
        /// <param name="count">Amount of segements we will split this rect into.</param>
        /// <param name="index">The index of a rect in after the split. </param>
        /// <returns></returns>
        public static Rect SplitToRow(Rect rect, int count, int index)
        {
            var averageWidth = (rect.width / count);

            Rect output = new Rect(
                rect.x + (averageWidth * index),
                rect.y,
                averageWidth,
                rect.height
                );

            return output;
        }

        /// <summary>
        /// Returns a split rect in a column at index.
        /// </summary>
        /// <param name="rect">The position and size of the original rect.</param>
        /// <param name="count">Amount of segements we will split this rect into.</param>
        /// <param name="index">The index of a rect in after the split. </param>
        /// <returns></returns>
        public static Rect SplitToColumn(Rect rect, int count, int index)
        {
            var averageHeight = (rect.height / count);

            Rect output = new Rect(
                rect.x,
                rect.y + (averageHeight * index),
                rect.width,
                averageHeight
                );

            return output;
        }

        /// <summary>
        /// Returns a split rect into a single column and width. Returns at index.
        /// </summary>
        /// <param name="rect">The position and size of the original rect.</param>
        /// <param name="columns">Amount of segements we will split this rects columns into.</param>
        /// <param name="rows">Amount of segements we will split this rects rows into.</param>
        /// <param name="index">The index of a rect in after the split. </param>
        /// <returns></returns>
        public static Rect SplitToRectIndex(Rect rect, int columns, int rows, Vector2 index)
        {
            var averageHeight = (rect.height / rows);
            var averageWidth = (rect.width / columns);

            Rect output = new Rect(
                rect.x + (averageWidth * index.x),
                rect.y + (averageHeight * index.y),
                averageWidth,
                averageHeight
                );

            return output;
        }
    }
}
