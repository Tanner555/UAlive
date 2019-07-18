using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(ContinueUnit), typeof(ContinueUnitDescriptor))]

namespace Lasm.UAlive
{
    public class ContinueUnitDescriptor : UnitDescriptor<ContinueUnit>
    {
        public ContinueUnitDescriptor(ContinueUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.continueUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.continueUnit);
        }
    }
}