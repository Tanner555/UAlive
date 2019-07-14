using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    [SpecialUnit]
    [UnitCategory("Nesting")]
    [UnitTitle("Get [Live]")]
    public sealed class GetterEntryUnit : PropertyEntryUnit
    {
        public Getter getter;
    }
}