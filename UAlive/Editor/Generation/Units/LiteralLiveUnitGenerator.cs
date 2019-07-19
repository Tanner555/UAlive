using Lasm.UAlive;
using Ludiq.Bolt;
using System.Collections.Generic;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(Literal), typeof(LiteralLiveUnitGenerator))]

namespace Lasm.UAlive
{
    public class LiteralLiveUnitGenerator : LiveUnitGenerator<Literal>
    {
        public LiteralLiveUnitGenerator(Literal unit) : base(unit)
        {
        }

        public override string Generate(int indent)
        {
            var output = string.Empty;

            output += Patcher.ActualValue(liveUnit.type, liveUnit.value);

            return output;
        }

        
    }
}
