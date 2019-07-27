using Lasm.UAlive;
using Ludiq.Bolt;
using Ludiq;
using System.Linq;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(BinaryOperatorUnit), typeof(BinaryOperatorLiveUnitGenerator))]

namespace Lasm.UAlive
{
    public class BinaryOperatorLiveUnitGenerator : LiveUnitGenerator<BinaryOperatorUnit>
    {
        public BinaryOperatorLiveUnitGenerator(BinaryOperatorUnit unit) : base(unit)
        {
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return string.Empty;
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            var outputString = string.Empty;

            if (output == liveUnit.result)
            {
                var a = liveUnit.a;
                var b = liveUnit.b;
                var result = liveUnit.result;
                var @operator = liveUnit.@operator;
                var entry = (unit.graph.units.ToListPooled().Where((x) => { return (x as EntryUnit) != null; }).ToListPooled()[0] as EntryUnit);
                var methodInput = entry as MethodInputUnit;

                GraphReference reference = null;

                if (methodInput != null) reference = GraphReference.New(methodInput.macro, BoltX.UnitGuids(methodInput.graph as FlowGraph), false);

                var aConnection = a.connection;
                var bConnection = b.connection;
                var aSource = aConnection.source;
                var bSource = bConnection.source;

                outputString += aConnection != null ? aSource.unit.CodeGenerator().GenerateValueOutput(aSource, 0) : Patcher.ActualValue(a.type, Flow.FetchValue(a, a.type, reference));

                outputString += CodeBuilder.Operator(@operator);

                outputString += bConnection != null ? bSource.unit.CodeGenerator().GenerateValueOutput(bSource, 0) : Patcher.ActualValue(b.type, Flow.FetchValue(b, b.type, reference));
            }

            return outputString;
        }
    }
}
