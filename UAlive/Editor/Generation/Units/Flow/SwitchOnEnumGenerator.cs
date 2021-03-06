﻿using System.Collections.Generic;
using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(SwitchOnEnum), typeof(SwitchOnEnumGenerator))]

namespace Lasm.UAlive
{
    public class SwitchOnEnumGenerator : LiveUnitGenerator<SwitchOnEnum>
    {
        public SwitchOnEnumGenerator(SwitchOnEnum unit) : base(unit)
        {
        }

        public override List<string> usingStatements => new List<string>() { liveUnit.enumType.Namespace };

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var output = string.Empty;

            if (input == liveUnit.enter)
            {
                var items = liveUnit.branches;
                var enumSource = liveUnit.@enum.connection.source;

                string @enum = null;

                if (liveUnit.@enum.connection != null && liveUnit.@enum.connection.sourceExists)
                {
                    @enum = enumSource.unit.CodeGenerator().GenerateValueOutput(enumSource, 0);
                }

                if (string.IsNullOrEmpty(@enum))
                {
                    return output;
                }

                output += CodeBuilder.Indent(indent) + "switch(" + @enum + ")" + "\n";
                output += CodeBuilder.Indent(indent) + "{" + "\n";

                var count = 0;

                foreach (ControlOutput enumBranch in items.Values)
                {
                    var enumBranchDestination = enumBranch.connection.destination;
                    output += CodeBuilder.Indent(indent + 1) + "case " + liveUnit.enumType.Name.ToString() + "." + enumBranch.key.Replace("%", string.Empty) + " :" + "\n";
                    output += enumBranch.connection != null ? enumBranchDestination.unit.CodeGenerator().GenerateControlInput(enumBranchDestination, indent + 2) : string.Empty;
                    output += "\n";
                    output += CodeBuilder.Indent(indent + 2) + "break;" + "\n" + (count == items.Values.Count - 1 ? string.Empty : "\n");
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
