using System.Collections.Generic;
using System;
using System.Linq;
using Ludiq;

namespace Lasm.UAlive
{
    [Serializable]
    public class Interfaces : List<Interface>
    {
        public Interface GetInterface(Type type)
        {
            return this.Where((@interface) => { return @interface.type == type; }).Single();
        }
    }
}