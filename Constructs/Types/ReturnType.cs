using System;
using Ludiq;
using Ludiq.Bolt;
using Ludiq.OdinSerializer;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class ReturnType
    {
        [OdinSerialize][Inspectable]
        public Type type = typeof(Void);
    }
}
