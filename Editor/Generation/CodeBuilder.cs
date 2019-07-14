using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using System;
using System.IO;
using UnityEditor;
using Lasm.Reflection;

namespace Lasm.UAlive.Generation
{
    public static class CodeBuilder
    {
        public static void Generate(this List<ObjectMacro> assets, bool isLive)
        {
            foreach (ObjectMacro asset in assets)
            {
                SetCompiledData(asset);

                var path = CodeBuilder.GetFinalPath(asset, asset.@namespace, asset.name);

                var kind = asset.kind;

                if (asset.script == null)
                {
                    using (var writer = new StreamWriter(path, false))
                    {
                        var output = string.Empty;

                        switch (kind)
                        {
                            case ObjectKind.Class:
                            output = isLive? ClassBuilder.Proxy(asset) : ClassBuilder.Compiled(asset);
                            break;

                            case ObjectKind.Enum:
                                output = isLive ? EnumBuilder.Proxy(asset) : EnumBuilder.CompiledEnum(asset);
                                break;
                        }

                        writer.Write(output);
                        writer.Close();
                    }
                }
                else
                {
                    using (var writer = new StreamWriter(AssetDatabase.GetAssetPath(asset.script), false))
                    {
                        var output = isLive ? ClassBuilder.Proxy(asset) : ClassBuilder.Compiled(asset);

                        writer.Write(output);
                        writer.Close();
                    }
                }

                if (asset.script == null) asset.script = AssetDatabase.LoadAssetAtPath<TextAsset>(path);

                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(asset.script), asset.name);
            }
        }

        public static string GetFinalPath(LudiqScriptableObject asset, string @namespace, string assetName)
        {
            string fileName = string.Empty;

            var filePath = GetFilePath(asset);

            if (((ObjectMacro)asset) != null)
            {
                if (!string.IsNullOrEmpty(((ObjectMacro)asset).name))
                {
                    fileName = ((ObjectMacro)asset).name;
                }
                else
                {
                    fileName = asset.name;
                }
            }
            else
            {
                throw new NullReferenceException();
            }

            if (!string.IsNullOrEmpty(((ObjectMacro)asset).name))
            {
                return filePath + Patcher.LegalVariableName(CodeBuilder.GetExtensionlessFileName(fileName), false) + ".cs";
            }

            return filePath + Patcher.LegalVariableName(assetName, false) + ".cs";
        }

        public static string GetFileName(LudiqScriptableObject asset)
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(asset);

            var lastSlashIndex = path.LastIndexOf("/") + 1;
            var fileName = path.Remove(0, lastSlashIndex);

            return fileName;
        }

        public static string GetFilePath(LudiqScriptableObject asset)
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(asset);

            var lastSlashIndex = path.LastIndexOf("/") + 1;
            var filePath = path.Remove(lastSlashIndex, path.Length - lastSlashIndex);

            return filePath;
        }

        public static string GetExtensionlessFileName(string fileName)
        {
            string extensionlessName = fileName;

            if (fileName.Contains("."))
            {
                var extensionIndexStart = fileName.IndexOf(".");
                extensionlessName = fileName.Remove(extensionIndexStart, fileName.Length - extensionIndexStart);
            }

            return extensionlessName.Replace(" ", string.Empty);
        }

        public static string Parameters(Dictionary<string, Type> parameters)
        {
            var output = string.Empty;
            var count = 0;

            foreach (KeyValuePair<string, Type> parameter in parameters)
            {
                output += parameter.Value + " " + parameter.Key + (count == parameters.Count - 1 ? string.Empty : ", ");
                count++;
            }

            return output;
        }

        public static string Scope(RootAccessModifier scope)
        {
            return Patcher.AddLowerUpperNeighboringSpaces(scope.ToString().ToLower());
        }

        public static string Scope(AccessModifier modifier)
        {
            return Patcher.AddLowerUpperNeighboringSpaces(modifier.ToString()).ToLower();
        }

        public static string Interfaces(List<Interface> interfaces, string inherits, bool addStartingComma)
        {
            var output = string.Empty;
            output += addStartingComma && !string.IsNullOrEmpty(inherits) ? ", " : string.Empty;

            for (int i = 0; i < interfaces.Count; i++)
            {
                output += interfaces[i].type.CSharpName();

                if (i < interfaces.Count - 1)
                {
                    output += ", ";
                }
            }

            return output;
        }

        public static string TryAddBaseTypeSeperator(string baseType, List<Interface> interfaces)
        {
            return (interfaces.Count == 0) && string.IsNullOrEmpty(baseType) ? string.Empty : " : ";
        }

        public static string Variables(List<Variable> variables, int indent)
        {
            var output = string.Empty;

            foreach (Variable variable in variables)
            {
                var asReadOnly = string.Empty;

                var type = variable.type.type;

                output += Indent(indent) + Scope(variable.scope) + VariableModifier(variable) + " " + Patcher.AsActualName(type) + " " + Patcher.LegalVariableName(variable.name, true);

                output += VariableScope(variable, indent);

                output += "\n";
            }

            return output;
        }

        private static string VariableModifier(Variable variable)
        {
            return (variable.fieldModifier == Lasm.UAlive.FieldModifier.None ? string.Empty : " " + variable.fieldModifier.ToString().ToLower());
        }

        private static string VariableScope(Variable variable, int indent)
        {
            if (variable.property.isProperty)
            {
                var output = string.Empty;

                if (variable.property.set && variable.property.get)
                {
                    output += " { ";

                    if (((GetterEntryUnit)((Ludiq.Bolt.FlowGraph)variable.property.setter.graph).units[0]).trigger.connection.destinationExists)
                    {

                    }
                    else
                    {
                        output += ((variable.property.getter.useScope) ? Scope(variable.property.getter.scope) : string.Empty) + " get; ";
                    }

                    if (((GetterEntryUnit)((Ludiq.Bolt.FlowGraph)variable.property.setter.graph).units[0]).trigger.connection.destinationExists)
                    {

                    }
                    else
                    {
                        output += ((variable.property.setter.useScope) ? Scope(variable.property.setter.scope) : string.Empty) + " set; ";
                    }

                    output += "/n } " + (variable.type.value == null ? string.Empty : " = " + variable.type.value.ValueCompiled(true)) + ";";
                }
                else
                {
                    if (variable.property.get && !variable.property.set) output += " { get; }" + (variable.type.value == null ? string.Empty : " = " + variable.type.value.ValueCompiled(true)) + ";";
                    else output += " { set; }" + (variable.type.value == null ? string.Empty : " = " + variable.type.value.ValueCompiled(true)) + ";";
                }

                return output;
            }
            else
            {
                return (variable.type.value == null ? string.Empty : " = " + variable.type.value.ValueCompiled(true)) + ";";
            }
        }

        public static string Constructor<TVariableData>(string typeName, List<TVariableData> collection, int indent) where TVariableData : Variable
        {
            var output = string.Empty;
            output += Indent(indent) + "public " + typeName + "(";

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].isReadOnly)
                {
                    output += Parameter(collection[i], (collection.Count == i + 1 ? true : false));
                }
                else
                {
                    output += Parameter(collection[i], (collection.Count == i + 1 ? true : false));
                }
            }

            output += ")";
            output += "\n";
            output += OpenBody(indent);
            output += "\n";

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].isReadOnly)
                {
                    output += Indent(indent + 1) + AssignReadOnly(collection[i]);

                    if (i + 1 < collection.Count)
                    {
                        output += "\n";
                    }
                }
            }

            output += "\n";
            output += CloseBody(indent);
            return output;
        }

        public static string AssignReadOnly<TVariableData>(TVariableData data) where TVariableData : Variable
        {
            var output = string.Empty;
            output += "this." + Patcher.LegalVariableName(data.name, true) + " = " + Patcher.LegalVariableName(data.name, true) + ";";
            return output;
        }

        public static string Parameter(Variable data, bool isLast)
        {
            var output = string.Empty;
            output += Patcher.AsActualName(data.type.type) + " " + Patcher.LegalVariableName(data.name, true) + (isLast ? "" : ", ");
            return output;
        }

        public static string OpenBody(int indent)
        {
            var output = string.Empty;

            output += Indent(indent) + "{";

            return output;
        }

        public static string OpenBody(int indent, int spaces)
        {
            var output = string.Empty;

            output += Indent(indent) + "{";

            for (int i = 0; i < spaces; i++)
            {
                output += "\n";
            }

            return output;
        }

        public static string CloseBody(int indent)
        {
            var output = string.Empty;

            output += Indent(indent) + "}";

            return output;
        }

        public static string CloseBody(int indent, int spaces)
        {
            var output = string.Empty;

            output += Indent(indent) + "}";

            for (int i = 0; i < spaces; i++)
            {
                output += "\n";
            }

            return output;
        }

        public static string Indent(int amount)
        {
            var output = string.Empty;

            for (int i = 0; i < amount; i++)
            {
                output += "    ";
            }

            return output;
        }

        public static string Operator(BinaryOperator @operator)
        {
            var output = " ";

            switch (@operator)
            {
                case BinaryOperator.Addition:
                    output += "+";
                    break;

                case BinaryOperator.And:
                    output += "&&";
                    break;

                case BinaryOperator.Division:
                    output += "/";
                    break;

                case BinaryOperator.Equality:
                    output += "==";
                    break;

                case BinaryOperator.ExclusiveOr:
                    output += "^";
                    break;

                case BinaryOperator.GreaterThan:
                    output += ">";
                    break;

                case BinaryOperator.GreaterThanOrEqual:
                    output += ">=";
                    break;

                case BinaryOperator.Inequality:
                    output += "!=";
                    break;

                case BinaryOperator.LeftShift:
                    output += "<<";
                    break;

                case BinaryOperator.LessThan:
                    output += "<";
                    break;

                case BinaryOperator.LessThanOrEqual:
                    output += "<=";
                    break;

                case BinaryOperator.Modulo:
                    output += "%";
                    break;

                case BinaryOperator.Multiplication:
                    output += "*";
                    break;

                case BinaryOperator.Or:
                    output += "||";
                    break;

                case BinaryOperator.RightShift:
                    output += ">>";
                    break;

                case BinaryOperator.Subtraction:
                    output += "-";
                    break;
            }

            output += " ";

            return output;
        }


        public static void SetCompiledData(ObjectMacro macro)
        {
            macro.last_BaseType = macro.baseType;
            macro.last_ClassModifier = macro.classModifier;
            macro.last_ConstructorCount = macro.constructors.Count;
            macro.last_InterfaceCount = macro.interfaces.Count;
            macro.last_Kind = macro.kind;
            macro.last_MethodCount = macro.methods.Count;
        }
    }
}