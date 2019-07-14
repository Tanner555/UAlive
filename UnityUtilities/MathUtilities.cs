using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lasm.UnityUtilities
{ 
    public static class MathUtilities
    {
        /// <summary>
        /// Remaps a float within one range, to its equivalent in another range.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue">Current ranges minimum value.</param>
        /// <param name="maxValue">Current ranges maximum value.</param>
        /// <param name="newMinValue">The new results minimum value.</param>
        /// <param name="newMaxValue">The new results maximum value.</param>
        /// <returns></returns>
        public static float Remap(this float value, float minValue, float maxValue, float newMinValue, float newMaxValue)
        {
            return ((value - minValue) / (maxValue - minValue)) * ((newMaxValue - newMinValue) + newMinValue);
        }

        /// <summary>
        /// Remaps an integer within one range, to its equivalent in another range.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minValue">Current ranges minimum value.</param>
        /// <param name="maxValue">Current ranges maximum value.</param>
        /// <param name="newMinValue">The new results minimum value.</param>
        /// <param name="newMaxValue">The new results maximum value.</param>
        /// <returns></returns>
        public static int Remap(this int value, int minValue, int maxValue, int newMinValue, int newMaxValue)
        {
            return ((value - minValue) / (maxValue - minValue)) * ((newMaxValue - newMinValue) + newMinValue);
        }
    }
}
