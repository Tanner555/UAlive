using Ludiq;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(Method), typeof(MethodDescriptor))]

namespace Lasm.UAlive
{
    public class MethodDescriptor : MacroDescriptor<Method, MacroDescription>
    {
        public MethodDescriptor(Method target) : base(target)
        {
        }
    }
}