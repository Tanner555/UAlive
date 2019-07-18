using System.Collections.Generic;
using System;
using System.Linq;
using Ludiq;

namespace Lasm.UAlive
{
    [Serializable][Inspectable]
    public class Methods : List<Method>
    {
        public Method GetMethod(string name)
        {
            return this.Where((method) => { return method.name == name; }).Single();
        }
    }
}