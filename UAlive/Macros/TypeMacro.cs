using Ludiq;
using System.Collections.Generic;
using Ludiq.Bolt;
using UnityEngine;
using System;

namespace Lasm.UAlive
{
    public abstract class TypeMacro : LudiqScriptableObject, IMacro
    {
        public string @namespace = string.Empty;

        public RootAccessModifier scope = RootAccessModifier.Public;

        public FlowGraph activeGraph = null;

        public IGraph graph { get => activeGraph; set => activeGraph = value as FlowGraph ; }

        public IGraph childGraph => graph;

        public bool isSerializationRoot => true;

        public UnityEngine.Object serializedObject => this;

        public abstract IEnumerable<object> aotStubs { get; }
    }
}