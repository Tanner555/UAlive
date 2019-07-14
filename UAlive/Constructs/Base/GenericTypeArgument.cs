using Ludiq;
using System;

namespace Lasm.UAlive
{
    [Serializable]
    public class GenericTypeArgument
    {
        [Serialize] public string name;
        [Serialize] public Type @interface;
        [Serialize] public Type baseType;
        [Serialize] public GenericTypeArgument argument;
        [Serialize] public TypeConstraint constraint;

        public void SetArgument()
        {
            if (argument != null)
            {
                argument.SetArgument();
            }
        }

        public string Generate()
        {
            var generate = string.Empty;

            generate += name;

            if (argument != null)
            {
                generate += "<";

                generate += argument.Generate();

                if (argument.argument != null)
                {
                    generate += ", ";
                }

                generate += ">";
            }

            return generate;
        }
    }
}