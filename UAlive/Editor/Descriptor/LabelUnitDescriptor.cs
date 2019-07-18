using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(LabelUnit), typeof(LabelUnitDescriptor))]

namespace Lasm.UAlive
{
    public class LabelUnitDescriptor : UnitDescriptor<LabelUnit>
    {
        public LabelUnitDescriptor(LabelUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.labelUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.labelUnit);
        }
    }
}