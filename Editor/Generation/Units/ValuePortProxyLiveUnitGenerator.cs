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

        public override string Generate(int indent)
        {
            var output = string.Empty;
            output += liveUnit.connections.ToListPooled()[0].source.unit.CodeGenerator().Generate(indent);
            return output;
        }
    }
}
