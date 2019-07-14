using Ludiq;
using Ludiq.OdinSerializer;
using System;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class StaticVariable
    {
        [Inspectable]
        [OdinSerialize]
        Type type { get; set; }
        [Inspectable]
        [OdinSerialize]
        string name { get; set; }
        [Inspectable]
        [OdinSerialize]
        GetSet getSet { get; set; }
    }
}