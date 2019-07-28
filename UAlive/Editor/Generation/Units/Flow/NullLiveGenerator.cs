using Lasm.UAlive;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(Null), typeof(NullLiveGenerator))]

namespace Lasm.UAlive
{
    public class NullLiveGenerator : LiveUnitGenerator<Null>
    {
        public NullLiveGenerator(Null unit) : base(unit)
        {

        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return string.Empty;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            if (output == liveUnit.@null)
            {
                return "null";
            }

            return string.Empty;
        }
    }
}
