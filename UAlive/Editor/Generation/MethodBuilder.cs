using System.Collections.Generic;
using Ludiq;
using System;
using Lasm.Reflection;

namespace Lasm.UAlive.Generation
{
    public static class MethodBuilder
    {
        public static string Methods(int indent, Methods methods, bool isLive)
        {
            var output = string.Empty;

            foreach (Method method in methods)
            {
                if (isLive)
                {
                    var parameters = ((MethodInputUnit)method.entry)._parameters;
                    var keys = ((MethodInputUnit)method.entry)._parameters.Keys.ToListPooled();
                    var methodParams = string.Empty;

                    for (int i = 0; i < parameters.Count; i++)
                    {
                        methodParams += keys[i] + ((i < parameters.Count - 1) ? ", " : string.Empty);
                    }

                    var shouldReturn = method.returns == null || method.returns != typeof(Lasm.UAlive.Void) && method.returns != typeof(void);

                    output += MethodHeader(indent, method.scope, method.modifier, method.returns, method.name, ((MethodInputUnit)method.entry)._parameters) + "\n";
                    output += CodeBuilder.OpenBody(indent, 1);
                    output += CodeBuilder.Indent(indent + 1) + "var methodName = \"" + method.name + "\";" + "\n";
                    output += CodeBuilder.Indent(indent + 1) + "macro = macro ?? TypeFinder.GetMacro(this.GetType().Name, ObjectKind.Class);" + "\n";
                    output += CodeBuilder.Indent(indent + 1) + method.name + "_Method = macro.methods.Where((_method)=>{ return _method.name == methodName; }).Single();" + "\n";
                    output += CodeBuilder.Indent(indent + 1) + GetInvoke(method, shouldReturn, methodParams);
                    output += CodeBuilder.CloseBody(indent, 2);
                }
                else
                {

                    output += "\n";
                    output += CodeBuilder.OpenBody(indent) + "\n";
                    output += method.entry.CodeGenerator().Generate(indent + 1) + "\n";
                    output += CodeBuilder.CloseBody(indent) + "\n";
                    output += "\n";
                }
            }

            return output;

            string GetInvoke(Method method, bool shouldReturn, string parameters)
            {
                var comma = string.IsNullOrEmpty(parameters) ? string.Empty : ", ";

                if (shouldReturn)
                {
                    if (method.returns.IsEnumerable())
                    {
                        return "yield return " + "Method.InvokeEnumerator(new MethodInstance(" + method.name + "_Method, this, false, returnObject: null" + comma + parameters + "));" + "\n";
                    }
                    else
                    {
                        return "return Method.InvokeReturn<" + method.returns.CSharpName() + ">(new MethodInstance(" + method.name + "_Method, this, false, returnObject: null" + comma + parameters + "));" + "\n";
                    }
                }
                
                return "Method.InvokeVoid(new MethodInstance(" + method.name + "_Method, this, false, returnObject: null" + comma + parameters + "));" + "\n";
            }
        }

        public static string MethodHeader(int depth, AccessModifier scope, MethodModifier modifier, Type returnType, string name, Dictionary<string, Type> parameters)
        {
            var header = string.Empty;
            var keys = parameters.Keys.ToListPooled();
            var values = parameters.Values.ToListPooled();
            var _parameters = string.Empty;

            for (int i = 0; i < parameters.Count; i++)
            {
                _parameters += ((i > 0) ?" " : string.Empty);
                _parameters += values[i].CSharpName() + " ";
                _parameters += Patcher.LegalVariableName(keys[i], false) + ((i != parameters.Count - 1) ? "," : string.Empty);
            }

            header += "\n";
            header += CodeBuilder.Indent(depth) + Patcher.AddLowerUpperNeighboringSpaces(scope.ToString()).ToLower() +
                " " + ((modifier == MethodModifier.None) ? string.Empty : modifier.ToString().ToLower()) +
                ((modifier == MethodModifier.None) ? string.Empty : " ") +
                (returnType == null || returnType == typeof(Void) ? typeof(void).CSharpName() : Ludiq.CSharpNameUtility.CSharpName(returnType)) + " " +
                name + "(" + _parameters + ")";

            return header;
        }

    }
}