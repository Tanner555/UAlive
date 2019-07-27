using Lasm.UAlive;
using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(Branch), typeof(BranchUnitGenerator))]

namespace Lasm.UAlive
{
    public class BranchUnitGenerator : LiveUnitGenerator<Branch>
    {
        public BranchUnitGenerator(Branch unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var output = string.Empty;

            var defaultValue = liveUnit.valueInputsData.GetValueOrDefault("condition").defaultValue;
            var logic = liveUnit.condition.connection != null ?
                liveUnit.condition.connection.sourceExists ?
                liveUnit.condition.connection.source.unit.CodeGenerator().GenerateValueOutput(liveUnit.condition.connection.source, 0) : Patcher.ActualValue(typeof(bool), defaultValue)
                : Patcher.ActualValue(typeof(bool), defaultValue);

            var hasTrue = liveUnit.ifTrue.connection != null;
            var hasFalse = liveUnit.ifTrue.connection != null;

            var trueDestination = liveUnit.ifFalse.connection?.destination;

            output += CodeBuilder.Indent(indent) + "if (" + logic + ") \n";
            output += CodeBuilder.Indent(indent) + "{\n";
            output += (hasTrue ? liveUnit.ifTrue.connection?.destination.unit.CodeGenerator().GenerateControlInput(liveUnit.ifTrue.connection?.destination, indent + 1) : string.Empty) + "\n";
            output += CodeBuilder.Indent(indent) + "}";

            if (hasFalse)
            {
                var falseDestination = liveUnit.ifFalse.connection.destination;

                output += "\n" + CodeBuilder.Indent(indent) + "else" + "\n";
                output += CodeBuilder.Indent(indent) + "{" + "\n";
                output += liveUnit.ifFalse.connection.destination.unit.CodeGenerator().GenerateControlInput(falseDestination, indent + 1) + "\n";
                output += CodeBuilder.Indent(indent) + "}" + "\n\n";
            }

            return output;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
