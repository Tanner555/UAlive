using Ludiq.Bolt;
using Lasm.Reflection;
using Ludiq;
using System.Linq;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    public abstract class LiveUnit : Unit
    {
        public EntryUnit entry;
        private protected int cyclesComplete = 0;
        private protected int refreshCycle = 10;
        private protected int unitCount => graph.units.Count;
        private protected int lastUnitCount;

        protected override void Definition()
        {
            if (entry == null || entry.graph != graph)
            {
                entry = entry ?? graph?.units?.OfType<EntryUnit>().First();
            }
        }

        public static bool ControlDestinationIsLiveUnit(ControlOutput source)
        {
            if (source.connection != null)
            {
                var connectedPort = source.connection.destination;
                if (connectedPort != null)
                {
                    if (Reflector.InheritsType(connectedPort.unit.GetType(), typeof(LiveUnit)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static LiveUnit ControlDestinationUnit(ControlOutput source)
        {
            var connectedPort = source.connection.destination.unit;

            if (connectedPort != null)
            {
                return connectedPort as LiveUnit;
            }

            return null;
        }

        public static bool ValueDestinationIsLiveUnit(ValueOutput source, int index)
        {
            var connectedPort = source.connections.ToListPooled()[index].destination;
            if (connectedPort != null)
            {
                if (Reflector.InheritsType(connectedPort.unit.GetType(), typeof(LiveUnit)))
                {
                    return true;
                }
            }

            throw new System.NullReferenceException("'" + source.key + "'" + " the port is not connected or the unit that it is connected to is not a LiveUnit. You can not use regular Unit types that don't derive from LiveUnit.");
        }


        public static bool ValueSourceIsLiveUnit(ValueInput source)
        {
            var connectedPort = source.connection.destination;
            if (connectedPort != null)
            {
                if (Reflector.InheritsType(connectedPort.unit.GetType(), typeof(LiveUnit)))
                {
                    return true;
                }
            }

            throw new System.NullReferenceException("'" + source.key + "'" + " the port is not connected or the unit that it is connected to is not a LiveUnit. You can not use regular Unit types that don't derive from LiveUnit.");
        }
    }
}