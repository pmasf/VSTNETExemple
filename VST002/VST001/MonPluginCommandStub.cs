using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace VST001
{
    public class MonPluginCommandStub : StdPluginCommandStub, IVstPluginCommandStub
    {
        protected override IVstPlugin CreatePluginInstance()
        {
            return new MonPlugin();
        }
    }
}
