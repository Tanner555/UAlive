using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(AsUnit), typeof(AsUnitDescriptor))]

namespace Lasm.UAlive
{
    public class AsUnitDescriptor : UnitDescriptor<AsUnit>
    {
        public AsUnitDescriptor(AsUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.asUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.asUnit);
        }
    }
}