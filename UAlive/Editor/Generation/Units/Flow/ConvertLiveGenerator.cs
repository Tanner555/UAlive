using Lasm.UAlive;
using Ludiq.Bolt;
using Ludiq;

[assembly: RegisterCodeGenerator(typeof(ConvertUnit), typeof(ConvertLiveGenerator))]

namespace Lasm.UAlive
{
    public class ConvertLiveGenerator : LiveUnitGenerator<ConvertUnit>
    {
        public ConvertLiveGenerator(ConvertUnit unit) : base(unit)
        {

        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return string.Empty;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            string outputString = string.Empty;

            if (output == liveUnit.output) {

                var inputConnection = liveUnit.input.connection;
                var inputSource = inputConnection?.source;

                outputString += "(" + liveUnit.type.CSharpName() + ")" + inputSource.unit.CodeGenerator().GenerateValueOutput(inputSource, indent);
            }

            return outputString;
        }
    }
}
