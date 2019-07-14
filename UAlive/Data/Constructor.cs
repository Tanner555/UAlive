using System;
using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [Inspectable]
    [Serializable]
    public sealed class Constructor : Macro<FlowGraph>
    {
        [Serialize]
        public AccessModifier scope = AccessModifier.Public;
        [Serialize]
        public ConstructorModifier special;
        [Serialize]
        public EntryUnit entry;
        [Serialize]
        public ObjectMacro owner;

        public void OnEnable()
        {
            if (entry != null)
            {
                if (!graph.units.Contains(entry)) InitializeGraph(ref entry);
            }
            else
            {
                InitializeGraph();
            }
        }

        public FlowGraph InitializeGraph()
        {
            var entry = new ConstructorUnit();
            graph = new FlowGraph();
            graph.controlAxis = Axis2.Vertical;
            graph.units.Add(entry);
            this.entry = entry;
            entry.macro = owner;
            entry.declaration = this;
            return graph;
        }

        public FlowGraph InitializeGraph(ref EntryUnit unit)
        {
            graph = new FlowGraph();
            graph.controlAxis = Axis2.Vertical;
            graph.units.Add(entry);
            entry.macro = owner;
            ((ConstructorUnit)entry).declaration = this;
            return graph;
        }
        
        public void Invoke()
        {
            var flow = Flow.New(GraphReference.New(this, true), false);
            flow.Invoke(entry.trigger);
        }
    }
}