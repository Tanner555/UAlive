using System;
using Ludiq.Bolt;
using Ludiq;
using System.Collections.Generic;
using Ludiq.OdinSerializer;
using UnityEngine;
using Lasm.Reflection;
using System.Collections;

namespace Lasm.UAlive
{
    [Inspectable]
    [Serializable]
    public sealed class Method : Macro<FlowGraph>
    {
        [Serialize]
        public List<string> attributes = new List<string>();
        [Serialize]
        public Type previousReturns = null;
        [Serialize]
        public AccessModifier scope = AccessModifier.Public;
        [Serialize]
        public MethodModifier modifier;
        [Serialize]
        public EntryUnit entry;
        [Serialize]
        public ObjectMacro owner;

        [OdinSerialize]
        [Inspectable]
        private Type _returns = typeof(Void);
        public Type returns { get { return _returns; } set { _returns = value; if (entry != null) { var methodInput = entry as MethodInputUnit;  if (methodInput != null) owner.UpdateAllGraphs(); } } }

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
            var entry = new MethodInputUnit();
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
            ((MethodInputUnit)entry).declaration = this;
            return graph;
        }

#if UNITY_EDITOR
        [Serialize]
        private List<string> last_Attributes = new List<string>();
        [Serialize]
        private Type last_ReturnType = typeof(Void);
#endif

        /// <summary>
        /// Invokes a method of a Live Type.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="liveType"></param>
        /// <param name="parameters"></param>
        public static void InvokeVoid(MethodInstance instance)
        {
            var reference = GraphReference.New(instance.method, true);
            var flow = Flow.New(reference, instance.isCoroutine);

            flow.AssignLocal<ILiveObject>("This", instance.target);

            SetParameters(instance, ((MethodInputUnit)instance.method.entry));

            TriggerFlow(flow, instance.method.entry.trigger);

            RemoveInstance(instance);
        }

        /// <summary>
        /// Invokes a method of a Live Type.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="liveType"></param>
        /// <param name="parameters"></param>
        public static TReturnType InvokeReturn<TReturnType>(MethodInstance instance)
        {
            var reference = GraphReference.New(instance.method, true);
            var flow = Flow.New(reference, instance.isCoroutine);

            flow.AssignLocal<ILiveObject>("This", instance.target);

            SetParameters(instance, ((MethodInputUnit)instance.method.entry));

            TriggerFlow(flow, instance.method.entry.trigger);

            TReturnType output = (TReturnType)instance.returnObject;

            RemoveInstance(instance);

            return output;
        }

        /// <summary>
        /// Invokes a method of a Live Type.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="liveType"></param>
        /// <param name="parameters"></param>
        public static object InvokeReturn(MethodInstance instance)
        {
            var reference = GraphReference.New(instance.method, true);
            var flow = Flow.New(reference, instance.isCoroutine);

            flow.AssignLocal<ILiveObject>("This", instance.target);

            SetParameters(instance, ((MethodInputUnit)instance.method.entry));

            TriggerFlow(flow, instance.method.entry.trigger);

            object output = instance.returnObject;

            RemoveInstance(instance);

            return output;
        }

        /// <summary>
        /// Invokes a method of a Live Type.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="liveType"></param>
        /// <param name="parameters"></param>
        public static IEnumerator InvokeEnumerator(MethodInstance instance)
        {
            var reference = GraphReference.New(instance.method, true);
            var flow = Flow.New(reference, instance.isCoroutine);

            flow.AssignLocal<ILiveObject>("This", instance.target);

            SetParameters(instance, ((MethodInputUnit)instance.method.entry));

            yield return TriggerFlowEnumerator(flow, instance.method.entry.trigger);

            IEnumerator output = (IEnumerator)instance.returnObject;

            RemoveInstance(instance);

            yield return output;
        }

        private static void SetParameters(MethodInstance instance, MethodInputUnit input)
        {
            input.parameterValues = instance.parameters;
        }

        private static void RemoveInstance(MethodInstance instance)
        {
            // We just invoked the output trigger to run the method. We can assume we can now free up the data.
            // So we need to remove the instance and the null object.
            instance.method.entry.targets.Remove(instance);
        }

        private static IEnumerator TriggerFlowEnumerator(Flow flow, ControlOutput trigger)
        {
            yield return flow.Coroutine(trigger);
        }

        private static void TriggerFlow(Flow flow, ControlOutput trigger)
        {
            flow.Invoke(trigger);
        }

        public bool IsVoid()
        {
            if (returns != null)
            {
                if (returns.IsVoid())
                {
                    return true;
                }

                return false;
            }
            else
            {
                throw new System.NullReferenceException("Return type is null.");
            }
        }

        public bool IsEnumerable()
        {
            if (returns != null)
            {
                if (returns.IsEnumerable())
                {
                    return true;
                }

                return false;
            }
            else
            {
                throw new System.NullReferenceException("Return type is null.");
            }
        }

        public static void SetInstanceValue(EntryUnit entry, Flow flow, ValueInput returns)
        {
            if (entry.targets.Count > 0)
            {
                entry.targets[entry.targets.Count - 1].returnObject = flow.GetValue(returns);
            }
        }
    }
    }