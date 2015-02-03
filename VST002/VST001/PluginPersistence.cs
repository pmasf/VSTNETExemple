using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;
using Jacobi.Vst.Framework.Plugin.IO;
using System.IO;
using System.Text;

namespace VST001
{

    internal class PluginPersistence : VstPluginPersistenceBase
    {
        private MonPlugin _plugin;

        /// <summary>
        /// Constructeur
        /// </summary>
        public PluginPersistence(MonPlugin plugin)
        {
            _plugin = plugin;
        }

        protected override VstProgramReaderBase CreateProgramReader(Stream input)
        {
            return new DelayProgramReader(_plugin, input, Encoding);
        }

        /// <summary>
        /// Gère la persistence du plugin
        /// </summary>
        private class DelayProgramReader : VstProgramReaderBase
        {
            private MonPlugin _plugin;

            public DelayProgramReader(MonPlugin plugin, Stream input, Encoding encoding)
                : base(input, encoding)
            {
                _plugin = plugin;
            }

            protected override VstProgram CreateProgram()
            {
                VstProgram program = new VstProgram(_plugin.ParameterFactory.Categories);

                _plugin.ParameterFactory.CreateParameters(program.Parameters);

                return program;
            }
        }
    }
}
