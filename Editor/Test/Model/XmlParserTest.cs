using System;
using System.Xml;
using MenuGenerator.Editor.Model;
using MenuGenerator.Editor.Model.Exceptions;
using NUnit.Framework;

namespace MenuGenerator.Editor.Test.Model
{
    public class XmlParserTest
    {
        [Test]
        public void TestSuccess()
        {
            const string validXmlStream = "<menu><firstLayer><secondLayer/></firstLayer><sibling/></menu>";
            XmlParser xmlParser = new();
            var document = xmlParser.ParseFile(validXmlStream);

            Assert.AreEqual("menu", document.FirstChild.Name);
            Assert.AreEqual("firstLayer", document.FirstChild.ChildNodes[0].Name);
            Assert.AreEqual("sibling", document.FirstChild.ChildNodes[1].Name);
            Assert.AreEqual("secondLayer", document.FirstChild.ChildNodes[0].FirstChild.Name);
        }

        [Test]
        public void TestEmptyStream()
        {
            XmlParser xmlParser = new();
            Assert.Throws<UninitializedException>(() => xmlParser.ParseFile(""));
        }

        [Test]
        public void TestInvalidXML()
        {
            XmlParser xmlParser = new();
            Assert.Throws<XmlException>(() => xmlParser.ParseFile("<menu></mune>"));
        }
    }
}