using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;


namespace VST001
{
    class ProcesseurAudio : IVstPluginAudioProcessor
    {
        /// <summary>
        /// Mon plugin
        /// </summary>
        private MonPlugin monPlugin;

        /// <summary>
        /// Taux d'échantillonage
        /// </summary>
        public float SampleRate { get; set; }
        
        /// <summary>
        /// Constructeur de la classe Processeur Audio
        /// </summary>
        /// <param name="monPlugin">Instance de MonPlugin</param>
        public ProcesseurAudio(MonPlugin monPlugin)
        {
            // On affecte l'instance de MonPlugin passée en paramètre à notre monPlugin local
            this.monPlugin = monPlugin;
        }

        #region IVstPluginAudioProcessor Members

        // Représente le nombre d'échantillons (par cannal) qui seront passés à la méthode Process
        public int BlockSize { get; set; }

        // Représente le nombre de canaux en entrée de notre plugin
        public int InputCount
        {
            get { return 2; }
        }

        // Représente le nombre de canaux en sortie de notre plugin
        public int OutputCount
        {
            get { return 2; }
        } 

        // Représente le nombre d'échantillons que notre plugin mettra en sortie après que l'entrée audio ne soit terminée
        public int TailSize
        {
            get { return 0; }
        }

        // Permet de définir la loi de panoramique utilisée par le plugin
        public bool SetPanLaw(VstPanLaw type, float gain)
        {
            return false;
        }

        /// <summary>
        /// Méthode qui effectue le traitement
        /// </summary>
        /// <param name="inputs">Entrées</param>
        /// <param name="outputs">Sorties</param>
        public void Process(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            // On affecte à nos variables de traitement la première entrée et la première sortie (canal mono 1)
            VstAudioBuffer input = inputs[0];
            VstAudioBuffer output = outputs[0];

            // Pour chaque échantillon en entrée on recopie le signal en sortie.
            for (int index = 0; index < output.SampleCount; index++)
            {
                output[index] = input[index];
            }

            // On effectue le même traitement avec la seconde entrée et la seconde sortie (canal mono 2)
            input = inputs[1];
            output = outputs[1];

            for (int index = 0; index < output.SampleCount; index++)
            {
                output[index] = input[index];
            }
        }

        #endregion
    }

}
