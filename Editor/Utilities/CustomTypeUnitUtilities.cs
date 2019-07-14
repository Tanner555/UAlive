using System;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;

namespace Lasm.UAlive
{
    public static class ObjectMacroUnitUtilities
    {
        public static bool TryGetParentUnit<TUnit>(Accessor baseAccessor, out TUnit parentUnit) where TUnit : class
        {
            if (baseAccessor.parent.value.GetType() == typeof(TUnit))
            {
                parentUnit = baseAccessor.parent.value as TUnit;
                return true;
            }

            parentUnit = null;
            return false;
        }
    }
}
