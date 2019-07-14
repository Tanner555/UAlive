using System;

namespace Lasm.UnityUtilities {
    public static class StringUtilities
    {
        /// <summary>
        /// Splits a string where a lowercase and uppercase are neighbors.
        /// </summary>
        /// <returns></returns>
        public static string AddLowerUpperNeighboringSpaces(this string value)
        {
            string last = string.Empty;
            string newString = string.Empty;
            

            for (int i = 0; i < value.Length; i++)
            {
                bool addedSpace = false;

                if (!string.IsNullOrEmpty(last))
                {
                    if (value[i].ToString() == value[i].ToString().ToUpper())
                    {
                        if (last == last.ToString().ToLower())
                        {
                            addedSpace = true;
                        }
                    }
                }

                if (addedSpace) newString += " ";

                newString += value[i];

                last = value[i].ToString();
            }

            return newString;
        }

        /// <summary>
        /// Splits a section of a string where a lowercase and uppercase are neighbors.
        /// </summary>
        /// <returns></returns>
        public static string AddLowerUpperNeighboringSpaces(this string value, int fromIndex, int toIndex)
        {
            string last = string.Empty;
            string newString = string.Empty;


            for (int i = fromIndex; i < toIndex; i++)
            {
                bool addedSpace = false;

                if (!string.IsNullOrEmpty(last))
                {
                    if (value[i].ToString() == value[i].ToString().ToUpper())
                    {
                        if (last == last.ToString().ToLower())
                        {
                            addedSpace = true;
                        }
                    }
                }

                if (addedSpace) newString += " ";

                newString += value[i];

                last = value[i].ToString();
            }

            return newString;
        }
    }  
}
