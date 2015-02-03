using Jacobi.Vst.Framework;

namespace VST001
{

    internal class PluginParameterFactory
    {

        /// <summary>
        /// Collection des catégories de paramètres
        /// </summary>
        public VstParameterCategoryCollection Categories = new VstParameterCategoryCollection();
        /// <summary>
        /// Collection des informations de paramètres
        /// </summary>
        public VstParameterInfoCollection ParameterInfos = new VstParameterInfoCollection();

        /// <summary>
        /// Constructeur
        /// </summary>
        public PluginParameterFactory()
        {
            VstParameterCategory paramCat = new VstParameterCategory();
            paramCat.Name = "Delay";

            Categories.Add(paramCat);
        }

        /// <summary>
        /// On remplit les informations
        /// </summary>
        public void CreateParameters(VstParameterCollection parameters)
        {
            foreach (VstParameterInfo paramInfo in ParameterInfos)
            {
                if (Categories.Count > 0 && paramInfo.Category == null)
                {
                    paramInfo.Category = Categories[0];
                }

                VstParameter param = new VstParameter(paramInfo);

                parameters.Add(param);
            }
        }
    }
}
