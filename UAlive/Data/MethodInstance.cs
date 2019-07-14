using System.Collections;

namespace Lasm.UAlive
{
    public class MethodInstance
    {
        public Method method;
        public ILiveObject target;
        public object[] parameters;
        public bool isCoroutine;
        public object returnObject;

        public MethodInstance(Method method, ILiveObject target, bool isCoroutine, object returnObject = null, params object[] parameters)
        {
            this.method = method;
            this.target = target;
            this.isCoroutine = isCoroutine;
            this.parameters = parameters;
            this.returnObject = returnObject;
        }
    }
}
