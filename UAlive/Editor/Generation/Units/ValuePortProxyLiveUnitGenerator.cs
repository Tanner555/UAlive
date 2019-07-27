using Lasm.UAlive;
using Ludiq.Bolt;
using Ludiq;
using System.Linq;

[assembly: RegisterCodeGenerator(typeof(UnitValuePortProxy), typeof(ValuePortProxyLiveUnitGenerator))]

namespace Lasm.UAlive
{
    public class ValuePortProxyLiveUnitGenerator : LiveUnitGenerator<UnitValuePortProxy>
    {
        public ValuePortProxyLiveUnitGenerator(UnitValuePortProxy unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return string.Empty;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            var outputString = string.Empty;
            var source = liveUnit.connections.ToListPooled()[0].source;

            outputString += source.unit.CodeGenerator().GenerateValueOutput(output, indent);

            return outputString;
        }
    }
}
