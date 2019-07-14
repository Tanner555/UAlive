using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UnityUtilities
{
    public static class ColorUtilities
    {
        /// <summary>
        /// Gets a color at index from start color to end color.
        /// </summary>
        /// <param name="start">The first color in the gradient</param>
        /// <param name="end">The last color in the gradient</param>
        /// <param name="amount">The amount of colors we can get a value back from</param>
        /// <param name="index">The current color selected based on the amount present.</param>
        /// <returns></returns>
        public static Color GetColorsBetween(Color start, Color end, int amount, int index)
        {
            Color colorDifference = new Color(
                       (end.r - start.r) / amount,
                       (end.g - start.g) / amount,
                       (end.b - start.b) / amount
                );

            if (index == 0)
            {
                return start;
            }

            return start + (colorDifference * (index + 1));
        }

        /// <summary>
        /// Gets the average between two colors.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Color GetColorBetween(Color start, Color end)
        {
            return GetColorsBetween(start, end, 3, 1);
        }


        public static Color Grey(this float dim)
        {
            return new Color(dim, dim, dim);
        }

        public static Color Grey(this float dim, float alpha)
        {
            return new Color(dim, dim, dim, alpha);
        }
    }
}
