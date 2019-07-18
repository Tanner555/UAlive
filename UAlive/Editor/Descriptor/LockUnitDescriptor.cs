using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(LockUnit), typeof(LockUnitDescriptor))]

namespace Lasm.UAlive
{
    public class LockUnitDescriptor : UnitDescriptor<LockUnit>
    {
        public LockUnitDescriptor(LockUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.lockUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.lockUnit);
        }
    }
}