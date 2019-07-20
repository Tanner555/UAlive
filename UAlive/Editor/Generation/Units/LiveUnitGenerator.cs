using System.Collections.Generic;
using Ludiq.Bolt;

namespace Lasm.UAlive
{
    public abstract class LiveUnitGenerator<TUnit> : CodeGenerator
        where TUnit : class, IUnit
    {
        public TUnit liveUnit => (TUnit)unit;

        public override List<string> usingStatements => base.usingStatements;

        public LiveUnitGenerator(TUnit unit)
        {
            this.unit = unit;
        }
    }
}
