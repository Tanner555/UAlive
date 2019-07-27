using Lasm.UAlive;
using Ludiq.Bolt;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(LockUnit), typeof(LockLiveGenerator))]

namespace Lasm.UAlive
{
    public class LockLiveGenerator : LiveUnitGenerator<LockUnit>
    {
        public LockLiveGenerator(LockUnit unit) : base(unit)
        {

        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            var outputString = string.Empty;

            if (input == liveUnit.enter)
            {
                var lockConnection = liveUnit.lockObject.connection;
                var lockedConnection = liveUnit.locked.connection;
                var lockSource = lockConnection?.source;
                var lockedDestination = lockedConnection?.destination;

                outputString += CodeBuilder.Indent(indent) + "lock(" + lockSource.unit.CodeGenerator().GenerateValueOutput(lockSource, 0) + ")";
                outputString += "\n" + CodeBuilder.OpenBody(indent) + "\n";
                outputString += CodeBuilder.Indent(indent + 1) + (lockedConnection == null ? string.Empty : lockedDestination.unit.CodeGenerator().GenerateControlInput(lockedDestination, indent + 1));
                outputString += "\n" + CodeBuilder.CloseBody(indent);
            }

            return outputString;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            return string.Empty;
        }
    }

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
            var outputString = string.Empty;

            if (output == liveUnit.@null)
            {
                return "null";
            }

            return outputString;
        }
    }
}
