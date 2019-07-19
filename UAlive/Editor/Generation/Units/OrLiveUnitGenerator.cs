using Lasm.UAlive;
using Ludiq.Bolt;
using Ludiq;
using System.Linq;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(Or), typeof(OrLiveUnitGenerator))]

namespace Lasm.UAlive
{
    public class OrLiveUnitGenerator : LiveUnitGenerator<Or>
    {
        public OrLiveUnitGenerator(Or unit) : base(unit)
        {
        }

        public override string Generate(int indent)
        {
            var output = string.Empty;
            var a = liveUnit.a;
            var b = liveUnit.b;
            var entry = (unit.graph.units.ToListPooled().Where((x) => { return (x as EntryUnit) != null; }).ToListPooled()[0] as EntryUnit);
            var methodInput = entry as MethodInputUnit;

            GraphReference reference = null;

            if (methodInput != null) reference = GraphReference.New(methodInput.macro, BoltX.UnitGuids(methodInput.graph as FlowGraph), false);

            output += a.connection != null ? a.connection.source.unit.CodeGenerator().Generate(0) : Patcher.ActualValue(a.type, Flow.FetchValue(a, a.type, reference));

            output += CodeBuilder.Operator(BinaryOperator.Or);

            output += b.connection != null ? b.connection.source.unit.CodeGenerator().Generate(0) : Patcher.ActualValue(b.type, Flow.FetchValue(b, b.type, reference));

            return output;
        }
    }
}
