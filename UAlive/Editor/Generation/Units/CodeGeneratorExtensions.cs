using Ludiq.Bolt;
using Ludiq;

namespace Lasm.UAlive
{
    public static class CodeGeneratorExtensions
    {
        public static ICodeGenerator CodeGenerator(this IUnit unit)
        {
            return CodeGeneratorProvider.instance.GetDecorator(unit);
        }

        public static ICodeGenerator CodeGenerator<TCodeGenerator>(this IUnit unit) where TCodeGenerator : ICodeGenerator
        {
            return CodeGeneratorProvider.instance.GetDecorator<TCodeGenerator>(unit);
        }
    }
}
