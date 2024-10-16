using System.IO;
using MenuGenerator.Editor.View.EditorInput;
using NUnit.Framework;
using UnityEngine;

namespace MenuGenerator.Editor.Test.View
{
    public class LocalFileValidatorTest
    {
        [Test]
        public void TestSuccess()
        {
            var validator = new LocalFileValidator();
            var result = validator.IsValid(Application.dataPath + "/MenuGenerator/Editor/Test/View/Components/test.xml");
            Assert.IsTrue(result);
        }

        [Test]
        public void TestFileNotFound()
        {
            var validator = new LocalFileValidator();
            var result = validator.IsValid(Application.dataPath + "/MenuGenerator/Editor/Test/View/Components/test.tar");
            var message = validator.GetMessage();

            Assert.IsFalse(result);
            Assert.AreEqual(LocalFileValidator.FILE_DOES_NOT_EXIST, message);
        }

        [Test]
        public void TestWrongFileType()
        {
            var filePath = Application.dataPath + "/MenuGenerator/Editor/Test/View/Components/test.xml";
            using var _ = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            var validator = new LocalFileValidator();
            var result = validator.IsValid(filePath);
            var message = validator.GetMessage();

            Assert.IsFalse(result);
            Assert.IsTrue(message.Contains(LocalFileValidator.ACCESS_VIOLATION));
        }
    }
}