using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ludiq;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class Yieldable
    {
        [Serialize][Inspectable]
        public bool canYield;
        [Serialize][Inspectable][InspectorToggleLeft]
        public bool @yield;
    }
}