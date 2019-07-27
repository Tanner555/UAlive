using Lasm.UAlive;
using Ludiq.Bolt;
using Ludiq;
using System.Linq;

[assembly: RegisterCodeGenerator(typeof(Negate), typeof(NegateLiveGenerator))]

namespace Lasm.UAlive
{
    public class NegateLiveGenerator : LiveUnitGenerator<Negate>
    {
        public NegateLiveGenerator(Negate unit) : base(unit)
        {
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            var outputString = string.Empty;

            if (output == liveUnit.output)
            {
                var inputConnection = liveUnit.input.connection;
                var inputSource = inputConnection.source;

                outputString += "!(" + inputSource.unit.CodeGenerator().GenerateValueOutput(inputSource, indent) + ")";
            }

            return outputString;
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return string.Empty;
        }
    }
}
