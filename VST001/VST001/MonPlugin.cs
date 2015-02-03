using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;


namespace VST001
{
    class MonPlugin : VstPluginWithInterfaceManagerBase
    {
        // Constructeur de MonPlugin
        public MonPlugin()
            : base("Mon Super Fun Plugin",
                new VstProductInfo("SFPlugin", "PAFactory", 1000),
                VstPluginCategory.RoomFx,
                VstPluginCapabilities.NoSoundInStop,
                0,
                0)  // Il faut mettre un identifiant unique ici !
        {
        }

        /// <summary>
        /// Permet de créer une instance de ProcesseurAudio
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            // Si notre instance de ProcesseurAudio est nulle on retourn une nouvelle instance prenant en paramètre notre objet.
            if (instance == null) return new ProcesseurAudio(this);

            // Sinon on appelle la création de base avec l'instance courante.
            return base.CreateAudioProcessor(instance);
        }
    }
}
