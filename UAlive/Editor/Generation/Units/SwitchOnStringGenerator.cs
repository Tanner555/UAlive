using System.Collections.Generic;
using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(SwitchOnString), typeof(SwitchOnStringGenerator))]

namespace Lasm.UAlive
{
    public class SwitchOnStringGenerator : LiveUnitGenerator<SwitchOnString>
    {
        public SwitchOnStringGenerator(SwitchOnString unit) : base(unit)
        {
        }

        public override List<string> usingStatements => new List<string>() { "System" };

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var output = string.Empty;

            if (input == liveUnit.enter)
            {
                var items = liveUnit.branches;

                string value = null;
                var valueConnection = liveUnit.selector.connection;
                var valueSource = valueConnection.source;

                if (liveUnit.selector.connection != null && liveUnit.selector.connection.sourceExists)
                {
                    value = valueSource.unit.CodeGenerator().GenerateValueOutput(valueSource, 0);
                }

                if (string.IsNullOrEmpty(value))
                {
                    return output;
                }

                output += CodeBuilder.Indent(indent) + "switch(" + @"""" + value + @"""" + ")" + "\n";
                output += CodeBuilder.Indent(indent) + "{" + "\n";

                var count = 0;

                foreach (KeyValuePair<string, ControlOutput> pair in items)
                {
                    var pairConnection = pair.Value.connection;
                    var pairDestination = pairConnection.destination;

                    output += CodeBuilder.Indent(indent + 1) + "case " + @"""" + pair.Key.ToString() + @"""" + " :" + "\n";
                    output += pairConnection != null ? pairDestination.unit.CodeGenerator().GenerateControlInput(pairDestination, indent + 2) : string.Empty;
                    output += "\n";
                    output += CodeBuilder.Indent(indent + 2) + "break;" + "\n" + (count == items.Count - 1 ? string.Empty : "\n");
                    count++;
                }

                output += CodeBuilder.Indent(indent) + "}";
            }

            return output;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
