using MenuGenerator.Editor.Model;
using MenuGenerator.Editor.Model.Constants;
using NUnit.Framework;
using UnityEngine;

namespace MenuGenerator.Editor.Test.Model
{
    public class PrefabManagerTest
    {
        [Test]
        public void TestSuccess()
        {
            var prefabManager = new PrefabManager();
            var result = prefabManager.GetPrefabByName(ModelName.EMPTY);

            Assert.IsInstanceOf<GameObject>(result);
            Assert.AreEqual(ModelName.EMPTY, result.name);
        }

        [Test]
        public void TestAssetNotFound()
        {
            var prefabManager = new PrefabManager();
            var result = prefabManager.GetPrefabByName("nonsense");

            Assert.IsNull(result);
        }
    }
}