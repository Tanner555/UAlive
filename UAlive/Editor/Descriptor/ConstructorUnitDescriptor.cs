using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(ConstructorUnit), typeof(ConstructorUnitDescriptor))]

namespace Lasm.UAlive
{
    public class ConstructorUnitDescriptor : UnitDescriptor<ConstructorUnit>
    {
        public ConstructorUnitDescriptor(ConstructorUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.constructorUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.constructorUnit);
        }
    }
}