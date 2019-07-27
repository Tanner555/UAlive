using Lasm.UAlive;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(MethodInputUnit), typeof(MethodInputUnitGenerator))]

namespace Lasm.UAlive
{
    public class MethodInputUnitGenerator : LiveUnitGenerator<MethodInputUnit>
    {
        public MethodInputUnitGenerator(MethodInputUnit unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var methodUnit = ((MethodInputUnit)unit);

            if (methodUnit.trigger.connection != null)
            {
                if (methodUnit.trigger.connection.destinationExists)
                {
                    var methodDestination = methodUnit.trigger.connection.destination;

                    return methodDestination.unit.CodeGenerator().GenerateControlInput(methodDestination, indent);
                }
            }

            return string.Empty;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
