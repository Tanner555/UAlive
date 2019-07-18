using Ludiq;
using Ludiq.Bolt;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(CompilerBranchUnit), typeof(CompilerBranchUnitDescriptor))]

namespace Lasm.UAlive
{
    public class CompilerBranchUnitDescriptor : UnitDescriptor<CompilerBranchUnit>
    {
        public CompilerBranchUnitDescriptor(CompilerBranchUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.processorDirectiveUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.processorDirectiveUnit);
        }
    }
}