using Jacobi.Vst.Framework;
using System.ComponentModel;

namespace VST001
{
    internal class Delay
    {
        // Buffer du délai
        private float[] _delayBuffer;

        // Index du buffer
        private int _bufferIndex;

        // Taille du buffer
        private int _bufferLength;

        // Paramètre de temps du délai
        private VstParameterManager _delayTimeMgr;

        // Paramètre de feedback du délai
        private VstParameterManager _feedbackMgr;

        // Paramètre de "dry" du délai (à quel niveau est le signal d'origine)
        private VstParameterManager _dryLevelMgr;

        // Paramètre de "wet" du délai (à quel niveau est le signal traité)
        private VstParameterManager _wetLevelMgr;

        /// <summary>
        /// Constructeur
        /// </summary>
        public Delay()
        {
            // On instancie une nouvelle collection d'informations pour les paramètres du délai
            _paramInfos = new VstParameterInfoCollection();

            #region Initialize Parameters

            // Paramètres du temps du délai
            VstParameterInfo paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "dt";
            paramInfo.Label = "Delay Time";
            paramInfo.ShortLabel = "T-Dly:";
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 1000;
            paramInfo.LargeStepFloat = 100.0f;
            paramInfo.SmallStepFloat = 1.0f;
            paramInfo.StepFloat = 10.0f;
            paramInfo.DefaultValue = 200f;
            _delayTimeMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            _paramInfos.Add(paramInfo);

            // Paramètre de feedback du délai
            paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "fb";
            paramInfo.Label = "Feedback";
            paramInfo.ShortLabel = "Feedbk:";
            paramInfo.LargeStepFloat = 0.1f;
            paramInfo.SmallStepFloat = 0.01f;
            paramInfo.StepFloat = 0.05f;
            paramInfo.DefaultValue = 0.2f;
            _feedbackMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            _paramInfos.Add(paramInfo);

            // Paramètre de "dry" du délai (à quel niveau est le signal d'origine)
            paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "dl";
            paramInfo.Label = "Dry Level";
            paramInfo.ShortLabel = "DryLvl:";
            paramInfo.LargeStepFloat = 0.1f;
            paramInfo.SmallStepFloat = 0.01f;
            paramInfo.StepFloat = 0.05f;
            paramInfo.DefaultValue = 0.8f;
            _dryLevelMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            _paramInfos.Add(paramInfo);

            // Paramètre de "wet" du délai (à quel niveau est le signal traité)
            paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "wl";
            paramInfo.Label = "Wet Level";
            paramInfo.ShortLabel = "WetLvl:";
            paramInfo.LargeStepFloat = 0.1f;
            paramInfo.SmallStepFloat = 0.01f;
            paramInfo.StepFloat = 0.05f;
            paramInfo.DefaultValue = 0.4f;
            _wetLevelMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            _paramInfos.Add(paramInfo);

            #endregion

            // On s'abonne à l'event de changement de la propriété de temps du délai
            _delayTimeMgr.PropertyChanged += new PropertyChangedEventHandler(_delayTimeMgr_PropertyChanged);
        }

        /// <summary>
        /// Event de changement de la propriété de temps du délai
        /// </summary>
        private void _delayTimeMgr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Recalcule la longueur du buffer si la valeur du paramètre a changé
            if (e.PropertyName == "CurrentValue")
            {
                VstParameterManager paramMgr = (VstParameterManager)sender;
                _bufferLength = (int)(paramMgr.CurrentValue * _sampleRate / 1000);
            }
        }

        private VstParameterInfoCollection _paramInfos;
        /// <summary>
        /// Collection des paramètres du délai
        /// </summary>
        public VstParameterInfoCollection ParameterInfos
        {
            get { return _paramInfos; }
        }

        private float _sampleRate;
        /// <summary>
        /// Taux d'échantillonage
        /// </summary>
        public float SampleRate
        {
            get { return _sampleRate; }
            set
            {
                _sampleRate = value;

                // on aloue la bonne taille au buffer pour avoir le délai maximum
                int bufferLength = (int)(_delayTimeMgr.ParameterInfo.MaxInteger * _sampleRate / 1000);
                _delayBuffer = new float[bufferLength];

                _bufferLength = (int)(_delayTimeMgr.CurrentValue * _sampleRate / 1000);
            }
        }

        /// <summary>
        /// Permet de traiter chaque échantillon
        /// </summary>
        public float ProcessSample(float sample)
        {
            if (_delayBuffer == null) return sample;

            // Traitement de la sortie
            float output = (_dryLevelMgr.CurrentValue * sample) + (_wetLevelMgr.CurrentValue * _delayBuffer[_bufferIndex]);

            // Traitement du buffer du délai
            _delayBuffer[_bufferIndex] = sample + (_feedbackMgr.CurrentValue * _delayBuffer[_bufferIndex]);

            _bufferIndex++;

            // Gestion de la position du buffer
            if (_bufferIndex >= _bufferLength)
            {
                _bufferIndex = 0;
            }

            return output;
        }

    }
}
