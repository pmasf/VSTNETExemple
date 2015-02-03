using Jacobi.Vst.Framework;

namespace VST001
{
    internal class Plugin : IVstPlugin
    {
        private IVstHost _hote;

        #region Membres de IVstPlugin

        private VstProductInfo _productInfo;
        public VstProductInfo ProductInfo
        {
            get
            {
                if (_productInfo == null)
                {
                    _productInfo = new VstProductInfo("SFPlugin", "PAFactory", 1000);
                }

                return _productInfo;
            }
        }

        public string Name
        {
            get { return "Mon Super Fun Plugin"; }
        }

        public Jacobi.Vst.Core.VstPluginCategory Category
        {
            get { return Jacobi.Vst.Core.VstPluginCategory.Unknown; }
        }

        public VstPluginCapabilities Capabilities
        {
            get { return VstPluginCapabilities.None; }
        }

        public int InitialDelay
        {
            get { return 0; }
        }

        public int PluginID
        {
            get { return 0; }
        }

        public void Open(IVstHost hote)
        {
            _hote = hote;
        }

        public void Suspend()
        {
        }

        public void Resume()
        {
        }

        #region IExtensibleObject Members

        public bool Supports<T>() where T : class
        {
            return (this is T);
        }

        public T GetInstance<T>() where T : class
        {
            return (this as T);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _hote = null;
        }

        #endregion

        
        #endregion
    }
}
