using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace VST001
{
    /// <summary>
    /// Cette classe gère l'interface du plugin
    /// </summary>
    class FxPluginInterfaceManager : PluginInterfaceManagerBase
    {
        // Représente notre plugin
        private MonPlugin _plugin;

        /// <summary>
        /// Constructeur
        /// </summary>
        public FxPluginInterfaceManager(MonPlugin plugin)
        {
            // Assignation du plugin passé en paramètre à notre plugin courant
            _plugin = plugin;
        }

        /// <summary>
        /// Permet de créer une instance par défaut du Processeur Audio ou d'utiliser l'existante
        /// </summary>
        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            // Si l'instance par défaut est nulle on la créé
            if (instance == null) return new ProcesseurAudio(_plugin);

            // Sinon on utilise l'instance par défaut déjà existante
            return instance;
        }

        /// <summary>
        /// Permet de créer une instance par défaut du Plugin Program ou d'utiliser l'existante
        /// </summary>
        protected override IVstPluginPrograms CreatePrograms(IVstPluginPrograms instance)
        {
            // Si l'instance par défaut est nulle on la créé
            if (instance == null) return new PluginPrograms(_plugin);

            // Sinon on utilise l'instance par défaut déjà existante
            return instance; 
        }

        /// <summary>
        /// Permet de créer une instance par défaut du Plugin Persistence ou d'utiliser l'existante
        /// </summary>
        protected override IVstPluginPersistence CreatePersistence(IVstPluginPersistence instance)
        {
            // Si l'instance par défaut est nulle on la créé
            if (instance == null) return new PluginPersistence(_plugin);

            // Sinon on utilise l'instance par défaut déjà existante
            return instance;
        }
    }
}
