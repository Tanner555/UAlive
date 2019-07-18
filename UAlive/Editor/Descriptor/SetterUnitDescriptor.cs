using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(SetterEntryUnit), typeof(SetterUnitDescriptor))]

namespace Lasm.UAlive
{
    public class SetterUnitDescriptor : UnitDescriptor<SetterEntryUnit>
    {
        public SetterUnitDescriptor(SetterEntryUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.setterUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.setterUnit);
        }
    }
}