using Lasm.UAlive;
using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(Branch), typeof(BranchBoltLiveGenerator))]

namespace Lasm.UAlive
{
    public class BranchBoltLiveGenerator : LiveUnitGenerator<Branch>
    {
        public BranchBoltLiveGenerator(Branch unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var outputString = string.Empty;

            var conditionConnection = liveUnit.condition.connection;
            var conditionSource = conditionConnection.source;

            var defaultValue = liveUnit.valueInputsData.GetValueOrDefault("condition").defaultValue;
            var logic = conditionConnection != null ? liveUnit.condition.connection.source.unit.CodeGenerator().GenerateValueOutput(conditionSource, 0) : Patcher.ActualValue(typeof(bool), defaultValue);

            var hasTrue = liveUnit.ifTrue.connection != null;
            var hasFalse = liveUnit.ifFalse.connection != null;

            var trueDestination = liveUnit.ifFalse.connection?.destination;

            outputString += CodeBuilder.Indent(indent) + "if (" + logic + ") \n";
            outputString += CodeBuilder.OpenBody(indent) + "\n";
            outputString += (hasTrue ? trueDestination.unit.CodeGenerator().GenerateControlInput(trueDestination, indent + 1) : string.Empty) + "\n";
            outputString += CodeBuilder.CloseBody(indent);

            if (hasFalse)
            {
                var falseDestination = liveUnit.ifFalse.connection.destination;

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
    }
}
