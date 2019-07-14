using System.Collections.Generic;

namespace Lasm.UAlive.Generation
{
    public class EnumBuilder
    {
        public static string EnumDeclaration(int indent, RootAccessModifier scope, string typeName, string attributes, List<Enum> items, bool isLive)
        {
            var output = string.Empty;
            output += attributes + (string.IsNullOrEmpty(attributes) ? string.Empty : "\n");
            output += CodeBuilder.Indent(indent) + CodeBuilder.Scope(scope) + " " + Patcher.ObjectTypeName(ObjectKind.Enum) + " " + typeName;
            output += "\n";
            output += CodeBuilder.Indent(indent) + "{";
            output += "\n";

            for (int i = 0; i < items.Count; i++)
            {
                output += CodeBuilder.Indent(indent + 1) + items[i].name + " = " + items[i].index.ToString();
                if (i != items.Count - 1) output += ",";
                output += "\n";
            }

            output += CodeBuilder.Indent(indent) + "}";

            return output;
        }

        public static string CompiledEnum(ObjectMacro asset)
        {
            var output = string.Empty;
            var @namespace = asset.@namespace;
            var name = asset.name;
            var scope = asset.scope;
            var items = asset.enumValues;
            var isNamespaceNull = string.IsNullOrEmpty(@namespace);
            var typeName = Patcher.TypeName(asset);

            var typeDeclaration = EnumDeclaration(
                isNamespaceNull ? 0 : 1,
                scope,
                typeName,
                string.Empty,
                items,
                false
                );

            if (!isNamespaceNull)
            {
                output += AssemblyBuilder.Namespace(typeDeclaration, @namespace);
                return output;
            }

            output += typeDeclaration;
            return output;
        }

        public static string Proxy(ObjectMacro asset)
        {
            return string.Empty;
        }
    }
}