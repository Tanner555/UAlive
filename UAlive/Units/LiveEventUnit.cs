using Ludiq;
using Ludiq.Bolt;
using System.Linq;

namespace Lasm.UAlive
{
    public abstract class LiveEventUnit<TArgs> : ManualEventUnit<TArgs>
    {
        protected EntryUnit entry;
        private protected int cyclesComplete = 0;
        private protected int refreshCycle = 10;
        private protected int unitCount => graph.units.Count;
        private protected int lastUnitCount;

        protected override void Definition()
        {
            base.Definition();

            if (entry == null)
            {
                entry = entry ?? graph?.units?.OfType<EntryUnit>().ToListPooled()[0];
            }
        }
    }
}