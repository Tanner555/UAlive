using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Ludiq.Bolt;
using UnityEditor;

namespace Lasm.UAlive.Generation
{
    public static class ClassBuilder
    {
        public static string Define(int indent, ObjectMacro liveObject, bool isLive)
        {
            return Declaration(indent,
                liveObject.scope,
                liveObject.name,
                liveObject.baseType.type.CSharpName(),
                liveObject.interfaces,
                string.Empty,
                string.Empty,
                liveObject.variables,
                string.Empty,
                string.Empty,
                liveObject.methods,
                isLive
                );
        }

        public static string Declaration(int indent, RootAccessModifier scope, ClassModifier modifier, string typeName, string inherits, List<Interface> interfaces, string attributes, string parameters, List<Variable> variables, string customVariables, string constructors, Methods methods, bool isLive)
        {
            var output = string.Empty;
            output += attributes + (string.IsNullOrEmpty(attributes) ? string.Empty : "\n");
            output += CodeBuilder.Indent(indent) + CodeBuilder.Scope(scope) + " " + ((modifier == ClassModifier.None) ? string.Empty : Patcher.AddLowerUpperNeighboringSpaces(modifier.ToString()).ToLower()) + " " + Patcher.ObjectTypeName(ObjectKind.Class) + " " + typeName + CodeBuilder.TryAddBaseTypeSeperator(inherits, interfaces) + inherits + CodeBuilder.Interfaces(interfaces, inherits, (interfaces.Count > 0) ? true : false);
            output += "\n";
            output += CodeBuilder.Indent(indent) + "{";
            output += "\n";
            output += CodeBuilder.Variables(variables, indent + 1);
            output += customVariables;
            output += (string.IsNullOrEmpty(customVariables) ? string.Empty : "\n");
            output += (!(string.IsNullOrEmpty(customVariables)) && string.IsNullOrEmpty(constructors) ? string.Empty : "\n");
            output += constructors;
            output += MethodBuilder.Methods(indent + 1, methods, isLive);
            output += CodeBuilder.Indent(indent) + "}";
            return output;
        }

        public static string Declaration(int indent, RootAccessModifier scope, ClassModifier modifier, string typeName, string inherits, List<Interface> interfaces, string attributes, string parameters, List<Variable> variables, string customVariables, string constructors, string preMethodCustomText, Methods methods, bool isLive)
        {
            var output = string.Empty;
            output += attributes + (string.IsNullOrEmpty(attributes) ? string.Empty : "\n");
            output += CodeBuilder.Indent(indent) + CodeBuilder.Scope(scope) + " " + ((modifier == ClassModifier.None) ? string.Empty : Patcher.AddLowerUpperNeighboringSpaces(modifier.ToString()).ToLower()) + " " + Patcher.ObjectTypeName(ObjectKind.Class) + " " + typeName + CodeBuilder.TryAddBaseTypeSeperator(inherits, interfaces) + inherits + CodeBuilder.Interfaces(interfaces, inherits, (interfaces.Count > 0) ? true : false);
            output += "\n";
            output += CodeBuilder.Indent(indent) + "{";
            output += "\n";
            output += CodeBuilder.Variables(variables, indent + 1);
            output += customVariables;
            output += (string.IsNullOrEmpty(customVariables) ? string.Empty : "\n");
            output += constructors;
            output += preMethodCustomText;
            output += string.IsNullOrEmpty(preMethodCustomText) ? string.Empty : "\n\n";
            output += MethodBuilder.Methods(indent + 1, methods, isLive);
            output += CodeBuilder.Indent(indent) + "}";
            return output;
        }

        public static string Declaration(int indent, RootAccessModifier scope, string typeName, string inherits, List<Interface> interfaces, string attributes, string parameters, List<Variable> variables, string customVariables, string constructors, Methods methods, bool isLive)
        {
            var output = string.Empty;
            output += attributes + (string.IsNullOrEmpty(attributes) ? string.Empty : "\n");
            output += CodeBuilder.Indent(indent) + CodeBuilder.Scope(scope) + " " + Patcher.ObjectTypeName(ObjectKind.Class) + " " + typeName + CodeBuilder.TryAddBaseTypeSeperator(inherits, interfaces) + inherits + CodeBuilder.Interfaces(interfaces, inherits, (interfaces.Count > 0) ? true : false);
            output += "\n";
            output += CodeBuilder.Indent(indent) + "{";
            output += "\n";
            output += CodeBuilder.Variables(variables, indent + 1);
            output += (string.IsNullOrEmpty(constructors) ? string.Empty : "\n");
            output += constructors;
            output += CodeBuilder.Indent(indent) + "}";
            return output;
        }

        public static string Compiled(ObjectMacro asset)
        {
            var output = string.Empty;
            var isNamespaceNull = string.IsNullOrEmpty(asset.@namespace);
            var usingStatements = new List<string>();
            var inherits = asset.baseType;
            var implements = asset.interfaces;
            var typeName = Patcher.TypeName(asset);
            var variables = asset.variables;
            var scope = asset.scope;
            var modifier = asset.classModifier;
            var @namespace = asset.@namespace;
            var name = asset.name;
            var methods = asset.methods;

            foreach (Method method in methods)
            {
                var units = method.graph.units;
                
                foreach(IUnit unit in units)
                {
                    var statements = unit.CodeGenerator().usingStatements;
                    foreach (string statement in statements)
                    {
                        AssemblyBuilder.UsingStatement(statement, usingStatements);
                    }
                }
            }

            AssemblyBuilder.UsingStatements(variables, usingStatements);
            AssemblyBuilder.UsingStatement(inherits.type, usingStatements);

            string typeDeclaration = ClassBuilder.Declaration(
                isNamespaceNull ? 0 : 1,
                scope,
                modifier,
                typeName,
                inherits.type.CSharpName(false) == "object" ? string.Empty : inherits.type.CSharpName(false),
                implements,
                string.Empty,
                string.Empty,
                variables,
                string.Empty,
                string.Empty,
                methods,
                false
            );

            output += AssemblyBuilder.GenerateUsingStatements(usingStatements);

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
            var output = string.Empty;
            var @namespace = asset.@namespace;
            var isNamespaceNull = string.IsNullOrEmpty(@namespace);
            var usingStatements = new List<string>();
            var inherits = asset.baseType;
            var implements = asset.interfaces.ToListPooled();
            var typeName = Patcher.TypeName(asset);
            var variables = asset.variables;
            var scope = asset.scope;
            var modifier = asset.classModifier;
            var name = asset.name;
            var methods = asset.methods;

            AssemblyBuilder.UsingStatement("System", usingStatements);
            AssemblyBuilder.UsingStatement("System.Linq", usingStatements);
            AssemblyBuilder.UsingStatement("System.Collections.Generic", usingStatements);
            AssemblyBuilder.UsingStatement("UnityEngine", usingStatements);
            AssemblyBuilder.UsingStatement("Lasm.UAlive", usingStatements);
            AssemblyBuilder.UsingStatement("Ludiq", usingStatements);
            AssemblyBuilder.UsingStatements(variables, usingStatements);
            AssemblyBuilder.UsingStatement(inherits.type, usingStatements);


            implements.Add(new Interface() { type = typeof(ILiveObject) });

            //var dynamicVariables = CodeBuilder.Indent(isNamespaceNull ? 1 : 2) + "[Serialize]" + "\n";
            //dynamicVariables += CodeBuilder.Indent(isNamespaceNull ? 1 : 2) + "protected Dictionary<string, object> variables = new Dictionary<string, object>();";

            var objectMacro = CodeBuilder.Indent(isNamespaceNull ? 1 : 2) + "protected ObjectMacro macro;" + "\n";

            var methodMacros = string.Empty;

            foreach(Method method in methods)
            {
                methodMacros += CodeBuilder.Indent(isNamespaceNull ? 1 : 2) + "protected Method " + method.name + "_Method;" + "\n";
            }



            string typeDeclaration = ClassBuilder.Declaration(
                isNamespaceNull ? 0 : 1,
                scope,
                modifier,
                typeName,
                inherits.type.CSharpName(false) == "object" ? string.Empty : inherits.type.CSharpName(false),
                implements,
                string.Empty,
                string.Empty,
                variables,
                objectMacro + methodMacros,
                string.Empty,
                string.Empty,
                methods,
                true
            );

            output += AssemblyBuilder.GenerateUsingStatements(usingStatements);

            output += "\n";
            output += "#if UNITY_EDITOR";
            output += "\n";
            output += "using UnityEditor;";
            output += "\n";
            output += "#endif";
            output += "\n \n";

            if (!isNamespaceNull)
            {
                output += AssemblyBuilder.Namespace(typeDeclaration, @namespace);
                return output;
            }

            output += typeDeclaration;

            return output;
        }
    }
}