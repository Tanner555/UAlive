using System.Collections.Generic;
using System;
using System.Linq;

namespace Lasm.UAlive
{
    [Serializable]
    public class Methods : List<Method>
    {
        public Method GetMethod(string name)
        {
            return this.Where((method) => { return method.name == name; }).Single();
        }
    }
}