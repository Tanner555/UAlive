using Lasm.UAlive;
using Lasm.UAlive.Generation;
using Ludiq.Bolt;

[assembly: RegisterCodeGenerator(typeof(For), typeof(ForLiveGenerator))]

namespace Lasm.UAlive
{
    public class ForLiveGenerator : LiveUnitGenerator<For>
    {
        private static int lastIteratorIndexAdjustment = 0;

        public ForLiveGenerator(For unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var outputString = string.Empty;

            if (input == liveUnit.enter)
            {
                var bodyConnection = liveUnit.body.connection;
                var bodyDestination = bodyConnection?.destination;
                var exitConnection = liveUnit.exit.connection;
                var exitDestination = exitConnection?.destination;
                var firstIndexConnection = liveUnit.firstIndex.connection;
                var firstIndexSource = firstIndexConnection?.source;
                var lastIndexConnection = liveUnit.lastIndex.connection;
                var lastIndexSource = lastIndexConnection?.source;
                var stepConnection = liveUnit.step.connection;
                var stepSource = stepConnection?.source;

                var firstIndex = firstIndexConnection == null ?
                    Patcher.ActualValue(typeof(int), liveUnit.firstIndex.data.defaultValue) :
                    firstIndexSource.unit.CodeGenerator().GenerateValueOutput(firstIndexSource, 0);

                var firstIndexValue = (int)liveUnit.firstIndex.data.defaultValue;

                var lastIndex = lastIndexConnection == null ?
                    Patcher.ActualValue(typeof(int), liveUnit.lastIndex.data.defaultValue) :
                    lastIndexSource.unit.CodeGenerator().GenerateValueOutput(lastIndexSource, 0);

                var lastIndexValue = (int)liveUnit.lastIndex.data.defaultValue;

                var step = stepConnection == null ?
                    Patcher.ActualValue(typeof(int), liveUnit.step.data.defaultValue) :
                    stepSource.unit.CodeGenerator().GenerateValueOutput(stepSource, 0);

                var iteratorName = char.ToString((char)(char.Parse("i") + lastIteratorIndexAdjustment));
                var lessOrGreater = firstIndexValue <= lastIndexValue ? " < " : " > ";
                var iteratorDirection = int.Parse(step) == 1 ? (firstIndexValue <= lastIndexValue ? "++" : "--") : (firstIndexValue <= lastIndexValue ? "+=" : "-=");

                lastIteratorIndexAdjustment++;

                var onBody = bodyConnection == null ? string.Empty : bodyDestination.unit.CodeGenerator().GenerateControlInput(bodyDestination, indent + 1);
                var hasExit = exitConnection != null;

                lastIteratorIndexAdjustment--;

                outputString += CodeBuilder.Indent(indent) + "for (int " + iteratorName + " = " + firstIndex + "; " + iteratorName + lessOrGreater + lastIndex + ";" + " " + (int.Parse(step) == 1 ? iteratorName + iteratorDirection : iteratorName + " " + iteratorDirection + " " + step) + ")" + "\n";
                outputString += CodeBuilder.OpenBody(indent) + "\n";
                outputString += onBody + "\n";
                outputString += CodeBuilder.CloseBody(indent);

                if (hasExit) outputString += "\n\n" + exitDestination.unit.CodeGenerator().GenerateControlInput(exitDestination, indent);
            }

            return outputString;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }
}
