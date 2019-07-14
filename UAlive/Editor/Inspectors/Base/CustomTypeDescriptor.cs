using Ludiq;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(ObjectMacro), typeof(ObjectMacroDescription))]

namespace Lasm.UAlive {
    public class ObjectMacroDescription : Descriptor<ObjectMacro, MacroDescription>
    {
        public ObjectMacroDescription(ObjectMacro target) : base(target)
        {
        }
    }
}