using System.Collections.Generic;
using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(SwitchOnIntegerNext), typeof(SwitchOnIntegerNextGenerator))]

namespace Lasm.UAlive
{
    public class SwitchOnIntegerNextGenerator : LiveUnitGenerator<SwitchOnIntegerNext>
    {
        public SwitchOnIntegerNextGenerator(SwitchOnIntegerNext unit) : base(unit)
        {
        }

        public override List<string> usingStatements => new List<string>() { "System" };

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var output = string.Empty;

            if (input == liveUnit.enter)
            {
                var items = liveUnit.cases;

                string value = null;

                var valueSource = liveUnit.value.connection.source;

                if (liveUnit.value.connection != null && liveUnit.value.connection.sourceExists)
                {
                    value = valueSource.unit.CodeGenerator().GenerateValueOutput(valueSource, 0);
                }

                if (string.IsNullOrEmpty(value))
                {
                    return output;
                }

                output += CodeBuilder.Indent(indent) + "switch(" + value + ")" + "\n";
                output += CodeBuilder.Indent(indent) + "{" + "\n";

                var count = 0;

                foreach (int @case in items)
                {
                    var caseDestination = liveUnit._cases[count].connection.destination;

                    output += CodeBuilder.Indent(indent + 1) + "case " + @case + " :" + "\n";
                    output += liveUnit._cases[count].connection != null ? caseDestination.unit.CodeGenerator().GenerateControlInput(caseDestination, indent + 2) : string.Empty;
                    output += "\n";
                    output += CodeBuilder.Indent(indent + 2) + "break;" + "\n" + (count == items.Count - 1 ? string.Empty : "\n");
                    count++;
                }

                output += CodeBuilder.Indent(indent) + "}\n";

                var nextDestination = liveUnit.next.connection.destination;

                if (liveUnit.next.connection != null) output += "\n" + nextDestination.unit.CodeGenerator().GenerateControlInput(nextDestination, indent);
            }

            return output;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
