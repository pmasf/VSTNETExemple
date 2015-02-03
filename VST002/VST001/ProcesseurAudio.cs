using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;


namespace VST001
{
    class ProcesseurAudio : IVstPluginAudioProcessor
    {
        private Delay _delay;
        /// <summary>
        /// Récupération du processeur Delai
        /// </summary>
        public Delay Delay { get { return _delay; } }

        /// <summary>
        /// Définit le taux d'échantillonage
        /// </summary>
        public float SampleRate
        {
            get { return _delay.SampleRate; }
            set { _delay.SampleRate = value; }
        }

        // Mon Plugin
        private MonPlugin monPlugin;

        /// <summary>
        /// Constructeur
        /// </summary>
        public ProcesseurAudio(MonPlugin monPlugin)
        {
            this.monPlugin = monPlugin;
            // On créé une nouvelle instance de notre délai
            this._delay = new Delay();
        }

        #region IVstPluginAudioProcessor Members

        public int BlockSize { get; set; }

        public int InputCount
        {
            get { return 2; }
        }

        public int OutputCount
        {
            get { return 2; }
        }

        public int TailSize
        {
            get { return 0; }
        }

        public bool SetPanLaw(VstPanLaw type, float gain)
        {
            return false;
        }
        #endregion

        /// <summary>
        /// Traite le signal en entrée pour y ajouter un délai
        /// </summary>
        public void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            // Récupération d'un canal en sortie
            VstAudioBuffer audioChannel = outChannels[0];

            // Pour chaque échantillon on fait le traitement
            for (int n = 0; n < audioChannel.SampleCount; n++)
            {
                audioChannel[n] = Delay.ProcessSample(inChannels[0][n]);
            }
        }

    }

}
