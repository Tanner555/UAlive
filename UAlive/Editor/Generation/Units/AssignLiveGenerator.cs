using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Lasm.Reflection;
using Lasm.UnityEditorUtilities;
using Ludiq.Bolt;
using Ludiq;
using UnityEngine;
using System.Linq;

[assembly: RegisterCodeGenerator(typeof(Assign), typeof(AssignLiveGenerator))]

namespace Lasm.UAlive
{
    public class AssignLiveGenerator : LiveUnitGenerator<Assign>
    {
        public AssignLiveGenerator(Assign unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            string output = string.Empty;

            var  inputSource = liveUnit.input.connection.source;
            var exitDestination = liveUnit.exit.connection.destination;

            var type = liveUnit.type.CSharpName();
            var name = Patcher.LegalVariableName(liveUnit.name, false);
            var value = string.Empty;
            if (liveUnit.input.connection != null && liveUnit.input.connection.sourceExists) value = inputSource.unit.CodeGenerator().GenerateValueOutput(inputSource, 0);
            var next = string.Empty;
            if (liveUnit.exit.connection != null && liveUnit.exit.connection.destinationExists) next = exitDestination.unit.CodeGenerator().GenerateControlInput(exitDestination, indent);

            return CodeBuilder.Indent(indent) + type + " " + name + " = " + value + ";" + (string.IsNullOrEmpty(next) ? "\n" : "\n\n" + next);
        }

        public override string GenerateValueOutput(ValueOutput outputPort, int indent)
        {
            var outputs = liveUnit.outputs.ToListPooled();
            if (outputPort == liveUnit.output)
            {
                return liveUnit.name;
            }

            return string.Empty;
        }
    }
}
