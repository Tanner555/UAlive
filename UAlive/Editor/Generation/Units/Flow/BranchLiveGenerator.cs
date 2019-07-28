using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(BranchLiveUnit), typeof(BranchLiveGenerator))]

namespace Lasm.UAlive
{
    public class BranchLiveGenerator : LiveUnitGenerator<BranchLiveUnit>
    {
        public BranchLiveGenerator(BranchLiveUnit unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var outputString = string.Empty;

            var conditionConnection = liveUnit.condition.connection;
            var conditionSource = conditionConnection.source;

            var defaultValue = liveUnit.valueInputsData.GetValueOrDefault("condition").defaultValue;
            var logic = conditionConnection != null ? liveUnit.condition.connection.source.unit.CodeGenerator().GenerateValueOutput(conditionSource, 0) : Patcher.ActualValue(typeof(bool), defaultValue);

            var hasTrue = liveUnit.@true.connection != null;
            var hasFalse = liveUnit.@false.connection != null;

            var trueDestination = liveUnit.@true.connection?.destination;

            outputString += CodeBuilder.Indent(indent) + "if (" + logic + ") \n";
            outputString += CodeBuilder.OpenBody(indent) + "\n";
            outputString += (hasTrue ? trueDestination.unit.CodeGenerator().GenerateControlInput(trueDestination, indent + 1) : string.Empty) + "\n";
            outputString += CodeBuilder.CloseBody(indent);

            if (hasFalse)
            {
                var falseDestination = liveUnit.@false.connection.destination;

                outputString += "\n" + CodeBuilder.Indent(indent) + "else" + "\n";
                outputString += CodeBuilder.OpenBody(indent) + "\n";
                outputString += falseDestination.unit.CodeGenerator().GenerateControlInput(falseDestination, indent + 1) + "\n";
                outputString += CodeBuilder.CloseBody(indent) + "\n";
            }

            return outputString;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }

        private string Condition()
        {
            var sourcePort = liveUnit.condition.connection.source;

            return (bool)liveUnit.condition.connection.sourceExists ? sourcePort.unit.CodeGenerator().GenerateValueOutput(sourcePort, 0) : string.Empty ;
        }
    }
}
