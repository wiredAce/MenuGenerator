using MenuGenerator.Editor.Model;

namespace MenuGenerator.Editor.Controller
{
    public class GeneratorController
    {
        private readonly XmlParser xmlParser = new();
        private MenuObjectBuilder objectBuilder = new();
        private GeneratorOutput generatorOutput = new();

        /// <summary>
        /// Main routine of the program.
        /// </summary>
        public void GenerateMenu(string fileStream)
        {
            //Parsing
            xmlParser.SetFileStream(fileStream);
            var document = xmlParser.ParseFile();
            var xmlMeta = xmlParser.GetMetaInformation();

            //Building
            objectBuilder.Build(document);
        }
    }
}