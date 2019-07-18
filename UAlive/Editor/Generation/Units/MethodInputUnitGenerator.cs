using Lasm.UAlive;
using Ludiq;

[assembly: RegisterCodeGenerator(typeof(MethodInputUnit), typeof(MethodInputUnitGenerator))]

namespace Lasm.UAlive
{
    public class MethodInputUnitGenerator : LiveUnitGenerator<MethodInputUnit>
    {
        public MethodInputUnitGenerator(MethodInputUnit unit) : base(unit)
        {
        }

        public override string Generate(int indent)
        {
            var methodUnit = ((MethodInputUnit)unit);
            if (methodUnit.trigger.connection != null)
            {
                if (methodUnit.trigger.connection.destinationExists)
                {
                    return methodUnit.trigger.connection.destination.unit.CodeGenerator().Generate(indent);
                }
            }

            return string.Empty;
        }
    }
}
