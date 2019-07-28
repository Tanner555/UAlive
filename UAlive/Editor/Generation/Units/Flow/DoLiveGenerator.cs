using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(DoUnit), typeof(DoLiveGenerator))]

namespace Lasm.UAlive
{
    public class DoLiveGenerator : LiveUnitGenerator<DoUnit>
    {
        public DoLiveGenerator(DoUnit unit) : base(unit)
        {

        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var outputString = string.Empty;

            if (input == liveUnit.enter)
            {
                var doConnection = liveUnit.@do.connection;
                var doDestination = doConnection?.destination;
                var conditionConnection = liveUnit.condition.connection;
                var conditionSource = conditionConnection?.source;

                outputString += CodeBuilder.Indent(indent) + "do" + "\n";
                outputString += CodeBuilder.OpenBody(indent) + "\n";

                if (doConnection != null)
                {
                    doDestination.unit.CodeGenerator().GenerateControlInput(doDestination, indent);
                }

                outputString += CodeBuilder.CloseBody(indent) + "\n";

                string whileCondition = string.Empty;

                whileCondition = conditionSource == null ? Patcher.ActualValue(typeof(bool), liveUnit.condition.data.defaultValue) : conditionSource.unit.CodeGenerator().GenerateValueOutput(conditionSource, indent + 1);

                outputString += CodeBuilder.Indent(indent) + "while (" + whileCondition + ");";

                var nextConnection = liveUnit.next.connection;
                var nextDestination = nextConnection?.destination;

                if (nextConnection != null)
                {
                    outputString += "\n" + nextDestination.unit.CodeGenerator().GenerateControlInput(nextDestination, indent);

                }
            }

            return outputString;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            var outputString = string.Empty;

            return string.Empty;
        }
    }
}
