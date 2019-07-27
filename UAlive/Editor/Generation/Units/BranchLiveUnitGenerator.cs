using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(BranchLiveUnit), typeof(BranchLiveUnitGenerator))]

namespace Lasm.UAlive
{
    public class BranchLiveUnitGenerator : LiveUnitGenerator<BranchLiveUnit>
    {
        public BranchLiveUnitGenerator(BranchLiveUnit unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var output = string.Empty;

            if (input == liveUnit.enter)
            {
                output += CodeBuilder.Indent(indent) + "if (" + ") \n";
                output += CodeBuilder.Indent(indent) + "{\n";
                output += CodeBuilder.Indent(indent + 1) + "\n";
                output += CodeBuilder.Indent(indent) + "}";
            }

            return output;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }

        private string Condition()
        {
            var sourcePort = liveUnit.condition.connection.source;

            return (bool)liveUnit.condition.connection.sourceExists ? sourcePort.unit.CodeGenerator().GenerateValueOutput(sourcePort, 0) : string.Empty ;
        }
    }
}
