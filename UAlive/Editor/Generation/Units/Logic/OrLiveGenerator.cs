﻿using Lasm.UAlive;
using Ludiq.Bolt;
using Ludiq;
using System.Linq;
using Lasm.UAlive.Generation;

[assembly: RegisterCodeGenerator(typeof(Or), typeof(OrLiveGenerator))]

namespace Lasm.UAlive
{
    public class OrLiveGenerator : LiveUnitGenerator<Or>
    {
        public OrLiveGenerator(Or unit) : base(unit)
        {
        }

        public override string GenerateValueOutput(ValueOutput output, int indent)
        {
            var outputString = string.Empty;

            if (output == liveUnit.result)
            {
                var a = liveUnit.a;
                var b = liveUnit.b;
                var entry = (unit.graph.units.ToListPooled().Where((x) => { return (x as EntryUnit) != null; }).ToListPooled()[0] as EntryUnit);
                var methodInput = entry as MethodInputUnit;

                GraphReference reference = null;

                if (methodInput != null) reference = GraphReference.New(methodInput.macro, BoltX.UnitGuids(methodInput.graph as FlowGraph), false);

                var aConnection = a.connection;
                var bConnection = b.connection;
                var aSource = aConnection.source;
                var bSource = bConnection.source;

                outputString += aConnection != null ? aSource.unit.CodeGenerator().GenerateValueOutput(aSource, 0) : Patcher.ActualValue(a.type, Flow.FetchValue(a, a.type, reference));

                outputString += CodeBuilder.Operator(BinaryOperator.Or);

                outputString += bConnection != null ? bSource.unit.CodeGenerator().GenerateValueOutput(bSource, 0) : Patcher.ActualValue(b.type, Flow.FetchValue(b, b.type, reference));
            }

            return outputString;
        }

        public override string GenerateControlInput(ControlInput input, int indent)
        {
            return string.Empty;
        }
    }
}
