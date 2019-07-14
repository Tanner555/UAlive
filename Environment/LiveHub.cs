using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Ludiq;
using Ludiq.OdinSerializer;

namespace Lasm.UAlive
{
    [Serializable]
    [Inspectable]
    public class LiveHub : LudiqBehaviour
    {
        [Serialize]
        [Inspectable][InspectorWide][InspectorLabel(null)]
        public Dictionary<string, ObjectMacro> types = new Dictionary<string, ObjectMacro>();
    }
}
