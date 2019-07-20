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

        public override string Generate(int indent)
        {
            var output = string.Empty;

            var items = liveUnit.branches;

            string value = null;

            if (liveUnit.selector.connection != null && liveUnit.selector.connection.sourceExists)
            {
                value = liveUnit.selector.connection.source.unit.CodeGenerator().Generate(0);
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
                output += CodeBuilder.Indent(indent + 1) + "case " + pair.Key.ToString() + " :" + "\n";
                output += pair.Value.connection != null ? pair.Value.connection.destination.unit.CodeGenerator().Generate(indent + 2) : string.Empty;
                output += "\n";
                output += CodeBuilder.Indent(indent + 2) + "break;" + "\n" + (count == items.Count - 1 ? string.Empty : "\n");
                count++;
            }

            output += CodeBuilder.Indent(indent) + "}";

            return output;
        }

    }
}
