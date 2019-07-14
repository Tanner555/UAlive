using Lasm.UAlive;
using Ludiq.Bolt;
using Ludiq;
using System.Linq;

[assembly: RegisterCodeGenerator(typeof(Negate), typeof(NegateLiveUnitGenerator))]

namespace Lasm.UAlive
{
    public class NegateLiveUnitGenerator : LiveUnitGenerator<Negate>
    {
        public NegateLiveUnitGenerator(Negate unit) : base(unit)
        {
        }

        public override string Generate(int indent)
        {
            var output = string.Empty;

            output += "!(" + liveUnit.input.connection.source.unit.CodeGenerator().Generate(indent) + ")";

            return output;
        }
    }
}
