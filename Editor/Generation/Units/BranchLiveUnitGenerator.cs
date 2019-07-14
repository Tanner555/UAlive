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

        public override string Generate(int indent)
        {
            var output = string.Empty;

            //output += CodeBuilder.Indent(indent) + "if (" +  ") \n";
            //output += CodeBuilder.Indent(indent) + "{\n";
            //output += CodeBuilder.Indent(indent + 1) + "\n";
            //output += CodeBuilder.Indent(indent) + "}";

            return output;
        }

        private string Condition()
        {
            return (bool)liveUnit.condition.connection.sourceExists ? liveUnit.condition.connection.source.unit.CodeGenerator().Generate(0) : string.Empty ;
        }
    }
}
