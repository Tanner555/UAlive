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

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return string.Empty;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            var outputString = string.Empty;

            outputString += Patcher.Literal(liveUnit.type, liveUnit.value);

            return outputString;
        }
    }
}
