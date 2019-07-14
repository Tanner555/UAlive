using Ludiq;
using Ludiq.Bolt;

namespace Lasm.UAlive
{
    public class CodeGeneratorProvider : SingleDecoratorProvider<IUnit, ICodeGenerator, RegisterCodeGeneratorAttribute>
    {
        static CodeGeneratorProvider()
        {
            instance = new CodeGeneratorProvider();
        }

        public static CodeGeneratorProvider instance { get; }

        protected override bool cache => false;

        public override bool IsValid(IUnit decorated)
        {
            return true;
        }
    }
}