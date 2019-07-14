using System.Collections.Generic;
using System;
using System.Linq;

namespace Lasm.UAlive
{
    [Serializable]
    public class Constructors : List<Constructor>
    {
        public Constructor GetConstructor(string name)
        {
            return this.Where((constructor) => { return constructor.name == name; }).Single();
        }
    }
}