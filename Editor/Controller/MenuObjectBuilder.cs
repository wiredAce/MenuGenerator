using System.Xml;
using JetBrains.Annotations;
using UnityEngine;
using MenuGenerator.Editor.Model;
using TMPro;

namespace MenuGenerator.Editor.Controller
{
    /// <summary>
    /// Places Menu Elements
    /// </summary>
    public class MenuObjectBuilder
    {
        private PrefabManager pm = new();

        public void Build(XmlDocument document)
        {
            var rootNode = document.FirstChild;

            if (null != rootNode["meta"])
                rootNode.RemoveChild(rootNode["meta"]);

            var rootObject = InitRoot();
            rootObject.name = "root";
            TraverseRecursively(rootNode, rootObject);
        }

        private Transform InitRoot()
        {
            var rootObject = Object.Instantiate(pm.GetPrefabByName("root"));
            var canvas = rootObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            return rootObject.transform;
        }

        private void TraverseRecursively(XmlNode node, Transform grandParent)
        {
            var parent = InstantiateFolder(node.Name, grandParent);

            foreach (XmlNode childNode in node.ChildNodes)
            {
                InstantiateButton(childNode.Name, parent);
                TraverseRecursively(childNode, parent);
            }
        }

        private Transform InstantiateFolder(string name, Transform parent)
        {
            var folder = Object.Instantiate(pm.GetPrefabByName("empty"), parent);
            folder.name = name;

            return folder.transform;
        }

        private void InstantiateButton(string name, [CanBeNull] Transform parent)
        {
            var button = Object.Instantiate(pm.GetPrefabByName("button"), parent);
            var buttonText = button.transform.GetChild(0);
            var rectTransform = button.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(150, 50);
            button.name = "to_" + name;
            buttonText.GetComponent<TextMeshProUGUI>().text = name;
        }
    }
}