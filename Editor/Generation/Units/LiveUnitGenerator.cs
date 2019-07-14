using System;
using Ludiq;
using Ludiq.Bolt;

namespace Lasm.UAlive
{
    public abstract class LiveUnitGenerator<TUnit> : CodeGenerator
        where TUnit : class, IUnit
    {
        public TUnit liveUnit => (TUnit)unit;

        public LiveUnitGenerator(TUnit unit)
        {
            this.unit = unit;
        }
    }
}
