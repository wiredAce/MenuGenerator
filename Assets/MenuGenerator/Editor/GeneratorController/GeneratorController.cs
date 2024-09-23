using MenuGenerator.Editor.ObjectBuilder;
using MenuGenerator.Editor.Output;
using MenuGenerator.Editor.Parser;

namespace MenuGenerator.Editor.GeneratorController
{
    public class GeneratorController
    {
        private readonly XmlParser xmlParser = new();
        private MenuObjectBuilder objectBuilder = new();
        private GeneratorOutput generatorOutput = new();

        public void GenerateMenu(string fileStream)
        {
            xmlParser.SetFileStream(fileStream);
            var document = xmlParser.ParseFile();
            var xmlMeta = xmlParser.GetMetaInformation();
        }
    }
}