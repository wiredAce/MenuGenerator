using MenuGenerator.Editor.Model;

namespace MenuGenerator.Editor.Controller
{
    public class GeneratorController
    {
        /// <summary>
        /// Collecting Info from user provided XML file
        /// </summary>
        private readonly XmlParser xmlParser = new();

        /// <summary>
        /// Places Menu Elements in hierarchy
        /// </summary>
        private readonly MenuObjectBuilder objectBuilder = new();

        /// <summary>
        /// Main routine of the program.
        /// </summary>
        public void GenerateMenu(string fileStream)
        {
            var document = xmlParser.ParseFile(fileStream);
            var xmlMeta = xmlParser.GetMetaInformation();

            objectBuilder.Build(document, xmlMeta);
        }
    }
}