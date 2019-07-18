using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(GetterEntryUnit), typeof(GetterUnitDescriptor))]

namespace Lasm.UAlive
{
    public class GetterUnitDescriptor : UnitDescriptor<GetterEntryUnit>
    {
        public GetterUnitDescriptor(GetterEntryUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.getterUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.getterUnit);
        }
    }
}