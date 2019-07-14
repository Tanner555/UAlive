using System;
using Ludiq;

namespace Lasm.UAlive
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class RegisterCodeGeneratorAttribute : Attribute, IRegisterDecoratorAttribute
    {
        public RegisterCodeGeneratorAttribute(Type decoratedType, Type decoratorType)
        {
            Ensure.That(nameof(decoratedType)).IsNotNull(decoratedType);
            Ensure.That(nameof(decoratorType)).IsNotNull(decoratorType);

            this.decoratedType = decoratedType;
            this.decoratorType = decoratorType;
        }

        public Type decoratedType { get; }
        public Type decoratorType { get; }
    }
}