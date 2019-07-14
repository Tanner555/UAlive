using Ludiq;
using Ludiq.Bolt;
using System;
using System.Collections.Generic;
using System.Collections;
using Lasm.Reflection;

namespace Lasm.UAlive
{
    public static class BoltX
    {
        public static void InvokeCustomEvent(GraphReference reference, string name, [AllowsNull] params object[] args)
        {
            reference.TriggerEventHandler<CustomEventArgs>(hook => hook.name == "Custom", new CustomEventArgs(name, args), parent => true, true);
        }

        public static void InvokeCustomEvent(FlowMacro macro, string name, [AllowsNull] params object[] args)
        {
            var reference = GraphReference.New(macro, true);
            reference.TriggerEventHandler<CustomEventArgs>(hook => hook.name == "Custom", new CustomEventArgs(name, args), parent => true, true);
        }

        public static GraphReference GetReference(FlowMacro macro)
        {
            return GraphReference.New(macro, false);
        }

        public static List<Guid> UnitGuids(FlowGraph graph)
        {
            var units = graph.units.ToListPooled();
            var output = new List<Guid>();

            for (int i = 0; i < units.Count; i++)
            {
                output.Add(units[i].guid);
            }

            return output;
        }
        public static List<Guid> ElementGuids(MergedGraphElementCollection elements)
        {
            var _elements = elements.ToListPooled();
            var output = new List<Guid>();

            for (int i = 0; i < _elements.Count; i++)
            {
                output.Add(_elements[i].guid);
            }

            return output;
        }
    }
}