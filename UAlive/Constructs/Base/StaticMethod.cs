using Ludiq;
using Ludiq.OdinSerializer;
using System;
using System.Collections.Generic;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class StaticMethod
    {
        [Inspectable]
        [OdinSerialize]
        Type returns;
        [Inspectable]
        [OdinSerialize]
        Dictionary<string, Type> parameters;
    }
}