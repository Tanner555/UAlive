using Lasm.UAlive;
using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(Branch), typeof(BranchUnitGenerator))]

namespace Lasm.UAlive
{
    public class BranchUnitGenerator : LiveUnitGenerator<Branch>
    {
        public BranchUnitGenerator(Branch unit) : base(unit)
        {
        }

        public override string Generate(int indent)
        {
            var output = string.Empty;

            //output += CodeBuilder.Indent(indent) + "if (" + liveUnit.condition.connection.source.unit.CodeGenerator().Generate(0) + ") \n";
            //output += CodeBuilder.Indent(indent) + "{\n";
            //output += CodeBuilder.Indent(indent + 1) + "\n";
            //output += CodeBuilder.Indent(indent) + "}";
            
            return output;
        }
    }
}
