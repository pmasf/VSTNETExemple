using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;


namespace VST001
{
    class MonPlugin : VstPluginWithInterfaceManagerBase
    {
        private FxPluginInterfaceManager _intfMgr;
        /// <summary>
        /// Paramètres du plugin
        /// </summary>
        public PluginParameterFactory ParameterFactory { get; private set; }

        public MonPlugin()
            : base("Mon Super Fun Plugin",
                new VstProductInfo("SFPlugin", "PAFactory", 1000),
                VstPluginCategory.RoomFx,
                VstPluginCapabilities.NoSoundInStop,
                0,
                0)
        {

            _intfMgr = new FxPluginInterfaceManager(this);
            ParameterFactory = new PluginParameterFactory();
            ProcesseurAudio audioProcessor = _intfMgr.GetInstance<ProcesseurAudio>();
            // On ajoute le délai aux paramètres du plugin
            ParameterFactory.ParameterInfos.AddRange(audioProcessor.Delay.ParameterInfos);
        }

        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            if (instance == null) return new ProcesseurAudio(this);

            return base.CreateAudioProcessor(instance);
        }

        #region IExtensibleObject Members

        public override T GetInstance<T>()
        {
            return _intfMgr.GetInstance<T>();
        }

        #endregion
    }
}
