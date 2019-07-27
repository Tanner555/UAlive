using System.Collections.Generic;
using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(SwitchOnStringNext), typeof(SwitchOnStringNextGenerator))]

namespace Lasm.UAlive
{
    public class SwitchOnStringNextGenerator : LiveUnitGenerator<SwitchOnStringNext>
    {
        public SwitchOnStringNextGenerator(SwitchOnStringNext unit) : base(unit)
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

                var valueConnection = liveUnit.value.connection;
                var valueSource = valueConnection.source;
                
                if (liveUnit.value.connection != null && liveUnit.value.connection.sourceExists)
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

                foreach (string @case in items)
                {
                    var caseConnection = liveUnit._cases[count].connection;
                    var caseDestination = caseConnection.destination;

                    output += CodeBuilder.Indent(indent + 1) + "case " + @"""" + @case + @"""" + " :" + "\n";
                    output += caseConnection != null ? caseDestination.unit.CodeGenerator().GenerateControlInput(caseDestination, indent + 2) : string.Empty;
                    output += "\n";
                    output += CodeBuilder.Indent(indent + 2) + "break;" + "\n" + (count == items.Count - 1 ? string.Empty : "\n");
                    count++;
                }

                output += CodeBuilder.Indent(indent) + "}\n";

                var nextConnection = liveUnit._cases[count].connection;
                var nextDestination = nextConnection.destination;

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
