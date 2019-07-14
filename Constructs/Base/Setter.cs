using Ludiq.Bolt;
using Ludiq;
using System;
using Ludiq.OdinSerializer;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public class Setter : IGraphParent, IGraphRoot
    {
        [Inspectable]
        public AccessModifier scope;
        [OdinSerialize]
        public bool useScope;

        [OdinSerialize]
        public ObjectMacro root;

        [Serialize]
        public FlowGraph _methodGraph;
        public FlowGraph methodGraph
        {
            get
            {
                if (_methodGraph == null)
                {
                    _methodGraph = GetGraph();
                }

                return _methodGraph;
            }

            set
            {
                _methodGraph = value;
            }
        }

        public IGraph childGraph => graph;

        public bool isSerializationRoot => false;

        public UnityEngine.Object serializedObject => root;

        public IGraph graph { get { return methodGraph; } set { methodGraph = value as FlowGraph; } }

        public IEnumerable<object> aotStubs
        {
            get
            {
                if (graph != null)
                {
                    if (graph.aotStubs?.ToListPooled().Count > 0)
                    {
                        return graph.aotStubs;
                    }
                }

                return null;
            }
        }

        public FlowGraph GetGraph()
        {
            FlowGraph graph = new FlowGraph();
            var entry = new SetterEntryUnit();
            graph.units.Add(entry);
            return graph;
        }
    }
}