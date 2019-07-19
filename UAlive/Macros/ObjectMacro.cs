using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Ludiq.Bolt;
using System;
using System.Linq;

namespace Lasm.UAlive
{
    [CreateAssetMenu(fileName = "New Object", menuName = "Bolt/UAlive/Object", order = 0)][Extract(false)]
    public sealed class ObjectMacro : TypeMacro
    {
        [Inspectable]
        [Serialize]
        public ClassModifier classModifier = ClassModifier.None;

        [Inspectable]
        [Serialize]
        public ObjectKind kind = ObjectKind.Class;

        [Inspectable]
        [InspectorWide]
        [Serialize]
        public BaseType baseType = new BaseType() { type = typeof(object) };

        [InspectorWide]
        [Serialize]
        public List<Interface> interfaces = new List<Interface>();
         
        [Serialize]
        [InspectorLabel(null)]
        public List<Variable> variables = new List<Variable>();

        [Serialize]
        [InspectorLabel(null)][Inspectable]
        public Methods methods = new Methods();

        [Serialize]
        [InspectorLabel(null)]
        public List<Constructor> constructors = new List<Constructor>();

        [Serialize]
        [Inspectable]
        public List<Enum> enumValues = new List<Enum>();

#if UNITY_EDITOR
        public TextAsset script;
        public bool interfacesOpen = true;
        public bool variablesOpen = true;
        public bool methodsOpen = true;

        #region LAST COMPILED DATA
        [Serialize]
        public ClassModifier last_ClassModifier = ClassModifier.None;
        [Serialize]
        public ObjectKind last_Kind = ObjectKind.Class;
        [Serialize]
        public BaseType last_BaseType = new BaseType() { type = typeof(MonoBehaviour) };
        [Serialize]
        public int last_InterfaceCount = 0;
        [Serialize]
        public int last_MethodCount = 0;
        [Serialize]
        public int last_ConstructorCount = 0;
        [Serialize]
        public int last_enumItemCount = 0;
        #endregion
#endif

        public override IEnumerable<object> aotStubs
        {
            get
            {
                var stubs = new List<object>();
                foreach (Method method in methods)
                { 
                    stubs.AddRange(method.graph.aotStubs);
                }

                return stubs;
            }
        }

        public void UpdateAllGraphs()
        {
            foreach (Method method in methods)
            {
                method.graph.Changed();
            }
        }

        public bool ShouldCompile()
        {
            if (last_BaseType != baseType) return true;
            if (last_ClassModifier != classModifier) return true;
            if (last_ConstructorCount != constructors.Count()) return true;
            if (last_Kind != kind) return true;
            if (last_MethodCount != methods.Count) return true;
            if (last_InterfaceCount != interfaces.Count) return true;

            return false;
        }
    }

    public enum ObjectKind
    {
        Class,
        Struct,
        Interface,
        Enum,
        Event
    }
}