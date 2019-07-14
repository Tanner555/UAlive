using System.Collections.Generic;
using System;

namespace Lasm.UAlive.Generation
{
    public class AssemblyBuilder
    {
        public static void UsingStatements(List<Variable> variables, List<string> statements)
        {
            foreach (Variable variable in variables)
            {
                var @namespace = variable.type.type.Namespace;

                if (!statements.Contains(@namespace))
                {
                    if (!string.IsNullOrEmpty(@namespace))
                    {
                        statements.Add(@namespace);
                    }
                }
            }
        }

        public static void UsingStatements(List<Type> collection, List<string> statements)
        {
            var output = string.Empty;

            for (int i = 0; i < collection.Count; i++)
            {
                var @namespace = collection[i].Namespace;

                if (!statements.Contains(@namespace))
                {
                    if (!string.IsNullOrEmpty(@namespace))
                    {
                        statements.Add(@namespace);
                    }
                }
            }
        }

        public static void UsingStatement(System.Type type, List<string> statements)
        {
            var @namespace = type.Namespace;

            if (!statements.Contains(@namespace))
            {
                statements.Add(@namespace);
            }
        }

        public static void UsingStatement(string statement, List<string> statements)
        {
            if (!statements.Contains(statement))
            {
                statements.Add(statement);
            }
        }

        public static string GenerateUsingStatements(List<string> statements)
        {
            var output = string.Empty;

            foreach (string statement in statements)
            {
                output += "using " + statement + ";\n";
            }

            return output + "\n";
        }

        public static string Namespace(string typeDeclaration, string @namespace)
        {
            var output = string.Empty;
            output += "namespace " + @namespace;
            output += "\n";
            output += CodeBuilder.OpenBody(0);
            output += "\n";
            output += typeDeclaration;
            output += "\n";
            output += CodeBuilder.CloseBody(0);

            return output;
        }

        public static string Attribute(string typeName, string parameters, int indent)
        {
            var output = string.Empty;

            output += CodeBuilder.Indent(indent) + "[" + typeName + "(" + parameters + ")]";

            return output;
        }
    }
}