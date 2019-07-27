using System.Collections.Generic;
using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(SwitchOnInteger), typeof(SwitchOnIntegerGenerator))]

namespace Lasm.UAlive
{
    public class SwitchOnIntegerGenerator : LiveUnitGenerator<SwitchOnInteger>
    {
        public SwitchOnIntegerGenerator(SwitchOnInteger unit) : base(unit)
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

                if (liveUnit.selector.connection != null && liveUnit.selector.connection.sourceExists)
                {
                    var selectorSource = liveUnit.selector.connection.source;
                    value = selectorSource.unit.CodeGenerator().GenerateValueOutput(selectorSource, 0);
                }

                if (string.IsNullOrEmpty(value))
                {
                    return output;
                }

                output += CodeBuilder.Indent(indent) + "switch(" + value + ")" + "\n";
                output += CodeBuilder.Indent(indent) + "{" + "\n";

                var count = 0;

                foreach (KeyValuePair<int, ControlOutput> pair in items)
                {
                    var pairConnection = pair.Value.connection;
                    var pairDestination = pairConnection.destination;

                    output += CodeBuilder.Indent(indent + 1) + "case " + pair.Key.ToString() + " :" + "\n";
                    output += pairConnection != null ? pair.Value.connection.destination.unit.CodeGenerator().GenerateControlInput(pairDestination, indent + 2) : string.Empty;
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
