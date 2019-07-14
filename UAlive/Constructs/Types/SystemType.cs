using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Ludiq.OdinSerializer;

namespace Lasm.UAlive
{
    [Serializable]
    public class SystemType
    {
        [OdinSerialize]
        public Type type;
        [OdinSerialize]
        public List<string> namespaceFilters;
        [OdinSerialize]
        public SystemTypeFilter filterType = SystemTypeFilter.ListOfNamespaces;
    }

    public enum SystemTypeFilter
    {
        ListOfNamespaces
    }
}