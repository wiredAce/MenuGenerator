using System.Xml;
using JetBrains.Annotations;

namespace MenuGenerator.Editor.Model
{
    public class XmlParser
    {
        /// <summary>
        /// Exception message for missing file stream
        /// </summary>
        private const string FILESTREAM_MISSING = "file stream must be injected before parsing";

        /// <summary>
        /// Exception message for unparsed document
        /// </summary>
        private const string DOCUMENT_MISSING = "document must be parsed before meta info is accessible";

        /// <summary>
        /// stream from user provided file
        /// </summary>
        private string fileStream;

        /// <summary>
        /// Finished xml document
        /// </summary>
        private XmlDocument document;

        /// <summary>
        /// Converts file stream to xml document
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UninitializedException"></exception>
        public XmlDocument ParseFile()
        {
            if (null == fileStream)
                throw new UninitializedException(FILESTREAM_MISSING);

            document = new XmlDocument();
            document.LoadXml(fileStream);

            return document;
        }

        /// <summary>
        /// Retrieves information from given xml-meta-tag
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UninitializedException"></exception>
        [CanBeNull]
        public XmlMeta GetMetaInformation()
        {
            if (null == document)
                throw new UninitializedException(DOCUMENT_MISSING);
            if (null == document.FirstChild["meta"])
                return null;

            XmlMeta xmlMeta = new();
            var metaNode = document.FirstChild["meta"];

            if (null != metaNode["menuName"]) xmlMeta.MenuName = metaNode["menuName"].InnerText;

            return xmlMeta;
        }


        public void SetFileStream(string _fileStream)
        {
            fileStream = _fileStream;
        }
    }
}