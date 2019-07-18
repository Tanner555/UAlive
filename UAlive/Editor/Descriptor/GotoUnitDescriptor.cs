using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(GotoUnit), typeof(GotoUnitDescriptor))]

namespace Lasm.UAlive
{
    public class GotoUnitDescriptor : UnitDescriptor<GotoUnit>
    {
        public GotoUnitDescriptor(GotoUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.gotoUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.gotoUnit);
        }
    }
}