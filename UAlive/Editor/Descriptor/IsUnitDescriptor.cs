using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(IsUnit), typeof(IsUnitDescriptor))]

namespace Lasm.UAlive
{
    public class IsUnitDescriptor : UnitDescriptor<IsUnit>
    {
        public IsUnitDescriptor(IsUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.isUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.isUnit);
        }
    }
}