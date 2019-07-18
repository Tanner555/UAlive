using Ludiq;
using Ludiq.Bolt;
using UnityEngine;
using Lasm.UAlive;

[assembly: RegisterDescriptor(typeof(ReturnUnit), typeof(ReturnUnitDescriptor))]

namespace Lasm.UAlive
{
    public class ReturnUnitDescriptor : UnitDescriptor<ReturnUnit>
    {
        public ReturnUnitDescriptor(ReturnUnit target) : base(target)
        {
        }

        protected override EditorTexture DefaultIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.returnUnit);
        }

        protected override EditorTexture DefinedIcon()
        {
            UAliveResources.CacheTextures();

            return EditorTexture.Single(UAliveResources.returnUnit); 
        }
    }
}