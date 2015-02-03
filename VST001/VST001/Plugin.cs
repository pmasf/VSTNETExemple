using Jacobi.Vst.Framework;

namespace VST001
{
    internal class Plugin : IVstPlugin
    {
        // Représente l'hôte de notre Plugin
        private IVstHost _hote;

        #region Membres de IVstPlugin

        // Diverses informations sur notre "produit" (qui n'est autre que notre plugin)
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

        // Le nom de notre produit qui s'affichera dans notre séquenceur pour le retrouver.
        public string Name
        {
            get { return "Mon Super Fun Plugin"; }
        }

        // La catégorie de notre plugin
        public Jacobi.Vst.Core.VstPluginCategory Category
        {
            get { return Jacobi.Vst.Core.VstPluginCategory.Unknown; }
        }

        // Les capacités de notre plugin
        public VstPluginCapabilities Capabilities
        {
            get { return VstPluginCapabilities.None; }
        }

        // La latence de notre plugin
        public int InitialDelay
        {
            get { return 0; }
        }

        // L'identifiant de notre plugin
        public int PluginID
        {
            get { return 0; }
        }

        // Méthode permettant d'affecter notre hôte courant
        public void Open(IVstHost hote)
        {
            _hote = hote;
        }

        // Méthode utilisée quand on arrête le plugin
        public void Suspend()
        {
        }

        // Méthode utilisée quand on reprend le traitement
        public void Resume()
        {
        }
        #endregion

        #region IExtensibleObject Members
        // Diverses méthodes utilitaires
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

        // On implémente IDisposable
        public void Dispose()
        {
            _hote = null;
        }

        #endregion


    }
}
