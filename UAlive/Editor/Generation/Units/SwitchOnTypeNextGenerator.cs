using System.Collections.Generic;
using Lasm.UAlive;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(SwitchOnTypeNext), typeof(SwitchOnTypeNextGenerator))]

namespace Lasm.UAlive
{
    public class SwitchOnTypeNextGenerator : LiveUnitGenerator<SwitchOnTypeNext>
    {
        public SwitchOnTypeNextGenerator(SwitchOnTypeNext unit) : base(unit)
        {
        }

        public override List<string> usingStatements => new List<string>() { "System" };

        public override string Generate(int indent)
        {
            var output = string.Empty;

            var items = liveUnit.cases;

            string value = null;

            if (liveUnit.value.connection != null && liveUnit.value.connection.sourceExists)
            {
                value = liveUnit.value.connection.source.unit.CodeGenerator().Generate(0);
            }

            if (string.IsNullOrEmpty(value))
            {
                return output;
            }

            output += CodeBuilder.Indent(indent) + "string typeString = " + @"""" + value.ToString() + @"""" + ";" + "\n\n";
            output += CodeBuilder.Indent(indent) + "switch(" + @"""" + value.ToString() + @"""" + ")" + "\n";
            output += CodeBuilder.Indent(indent) + "{" + "\n";

            var count = 0;

            foreach (System.Type @case in items)
            {
                output += CodeBuilder.Indent(indent + 1) + "case " + @"""" + @case + @"""" + " :" + "\n";
                output += liveUnit._cases[count].connection != null ? liveUnit._cases[count].connection.destination.unit.CodeGenerator().Generate(indent + 2) : string.Empty;
                output += "\n";
                output += CodeBuilder.Indent(indent + 2) + "break;" + "\n" + (count == items.Count - 1 ? string.Empty : "\n");
                count++;
            }

            output += CodeBuilder.Indent(indent) + "}\n";

            if (liveUnit.next.connection != null) output += "\n" + liveUnit.next.connection.destination.unit.CodeGenerator().Generate(indent);

            return output;
        }
    }
}
