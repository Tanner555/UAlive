using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Ludiq.Bolt;
using Lasm.Reflection;

namespace Lasm.UAlive
{
    public static class Proxy
    {
        public static Coroutine Routine(IEnumerator enumerator)
        {
            return CoroutineRunner.instance.StartCoroutine(enumerator);
        }

        public static IEnumerator Yield(Flow flow, ControlOutput next, bool isCoroutine)
        {
            if (isCoroutine) yield return Flow.New(flow.stack.AsReference(), true).Coroutine(next);
        }
    }
}
