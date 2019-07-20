using Ludiq;

namespace Lasm.UAlive.Generation
{
    public static class Patcher
    {
        public static string LegalVariableName(string name, bool underscoreMiddleSpaces)
        {
            var output = name;
            output = RemoveStartingSpaces(output);
            output = RemoveIllegalVariableCharacters(output, underscoreMiddleSpaces);
            return output;
        }

        public static string RemoveStartingSpaces(string text)
        {
            var output = text;
            var length = output.Length;
            int finalCount = 0;

            for (int i = 0; i < length; i++)
            {
                if (output[i].ToString() != " ")
                {
                    break;
                }

                finalCount++;
            }

            output = output.Remove(0, finalCount);

            return output;
        }

        public static string RemoveIllegalVariableCharacters(string name, bool underscoreSpaces)
        {
            var output = string.Empty;
            var length = name.Length;

            for (int i = 0; i < length; i++)
            {
                if (name[i].ToString() == null) break;
                if (char.IsLetter(name[i]))
                {
                    output += name[i].ToString();
                }
                else
                {
                    if (char.IsWhiteSpace(name[i]))
                    {
                        if (underscoreSpaces)
                            output += "_";
                    }
                }
            }

            return output;
        }

        public static string AsActualName(System.Type type)
        {
            return type.CSharpName(true);
        }

        public static string ActualValue(System.Type type, object value, bool isNew = false)
        {
            var output = string.Empty;

            if (type.IsPrimitive)
            {
                if (type == typeof(float))
                {
                    output += value.ToString() + "f";
                    return output;
                }

                if (type == typeof(string))
                {
                    output += @"""" + value.ToString() + @"""";
                    return output;
                }

                if (type == typeof(bool))
                {
                    output += value.ToString().ToLower();
                    return output;
                }

                output += value.ToString();
                return output;
            }

            if (type.IsEnum)
            {
                if (isNew)
                {
                    output += "new " + type.Name + "()" + "{ " + value.ToString() + " }";
                }
                else
                {
                    output += type.Name + "." + value.ToString();
                }

                return output;
            }

            if (isNew)
            {
                return "new " + type.Name + "(" + value.ToString() + ")";
            }

            return value.ToString();
        }

        public static string Literal(System.Type type, object value)
        {
            var output = string.Empty;

            if (type.IsPrimitive)
            {
                if (type == typeof(float))
                {
                    output += value.ToString() + "f";
                    return output;
                }

                if (type == typeof(string))
                {
                    output += @"""" + value.ToString() + @"""";
                    return output;
                }

                if (type == typeof(bool))
                {
                    output += value.ToString().ToLower();
                    return output;
                }

                if (type == typeof(char))
                {
                    output += @"'" + value + @"'";
                    return output;
                }

                output += value.ToString();
                return output;
            }

            if (type.IsEnum)
            {
                output += "new " + type.Name + "()" + "{ " + value.ToString() + " }";
                return output;
            }

            return "new " + type.Name + "(" + value.ToString() + ")";

            return value.ToString();
        }

        /// <summary>
        /// Splits a string where a lowercase and uppercase are neighbors.
        /// </summary>
        /// <returns></returns>
        public static string AddLowerUpperNeighboringSpaces(string value)
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
        public static string AddLowerUpperNeighboringSpaces(string value, int fromIndex, int toIndex)
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

        public static string TypeName(ObjectMacro asset)
        {
            return Patcher.LegalVariableName((string.IsNullOrEmpty(asset.name) ? CodeBuilder.GetExtensionlessFileName(CodeBuilder.GetFileName(asset)).Replace(" ", string.Empty) : asset.name).Replace(" ", string.Empty), false);
        }


        public static string ObjectTypeName(ObjectKind objectType)
        {
            return objectType.ToString().ToLower();
        }
    }
}