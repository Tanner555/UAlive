using Ludiq.Bolt;

namespace Lasm.UAlive
{
    [SpecialUnit]
    [UnitCategory("Nesting")]
    [UnitTitle("Set [Live]")]
    public sealed class SetterEntryUnit : PropertyEntryUnit
    {
        public Setter setter;
    }
}