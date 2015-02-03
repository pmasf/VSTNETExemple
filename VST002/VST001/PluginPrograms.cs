using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace VST001
{
    /// <summary>
    /// Gestion des presets du plugin
    /// </summary>
    class PluginPrograms : VstPluginProgramsBase
    {
        MonPlugin _plugin;

        /// <summary>
        /// Constructeur
        /// </summary>
        public PluginPrograms(MonPlugin plugin)
        {
            _plugin = plugin;
        }

        /// <summary>
        /// Initialisation des presets du plugin
        /// </summary>
        protected override VstProgramCollection CreateProgramCollection()
        {
            // On instancie une nouvelle collection de presets
            VstProgramCollection programs = new VstProgramCollection();

            // On créé et ajoute les presets un à un à la collection
            VstProgram prog = new VstProgram(_plugin.ParameterFactory.Categories);
            prog.Name = "Mon Premier Preset";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram(_plugin.ParameterFactory.Categories);
            prog.Name = "Mon Deuxieme Preset";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            prog = new VstProgram(_plugin.ParameterFactory.Categories);
            prog.Name = "Mon Troisieme Preset";
            _plugin.ParameterFactory.CreateParameters(prog.Parameters);

            programs.Add(prog);

            return programs;
        }
    }
}
