using System.Xml;
using JetBrains.Annotations;
using MenuGenerator.Behaviours;
using UnityEngine;
using MenuGenerator.Editor.Model;
using MenuGenerator.Editor.Model.Constants;
using TMPro;

namespace MenuGenerator.Editor.Controller
{
    /// <summary>
    /// Places Menu Elements
    /// </summary>
    public class MenuObjectBuilder
    {
        private PrefabManager pm = new();

        /// <summary>
        /// Initializes basic structure and starts recursive traversal
        /// </summary>
        /// <param name="document"></param>
        public void Build(XmlDocument document)
        {
            var rootNode = document.FirstChild;

            if (null != rootNode["meta"])
                rootNode.RemoveChild(rootNode["meta"]);

            var rootObject = InitRoot();
            rootObject.name = "root";
            //todo/marbro es braucht noch nen eventsystem sonst zerbricht alles
            InitNavigator(rootObject).GetComponent<Navigator>();
            TraverseRecursively(rootNode, rootObject);
        }

        /// <summary>
        /// Initializes the parent object for the menu
        /// </summary>
        /// <returns></returns>
        private Transform InitRoot()
        {
            var rootObject = Object.Instantiate(pm.GetPrefabByName(ModelName.ROOT));
            var canvas = rootObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            return rootObject.transform;
        }

        /// <summary>
        /// Initializes the helper object for navigating between menu layers
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private Transform InitNavigator(Transform root)
        {
            var rootObject = Object.Instantiate(pm.GetPrefabByName(ModelName.NAVIGATOR), root);

            return rootObject.transform;
        }

        /// <summary>
        /// Recursively traverses the XML document and instantiates the menu elements
        /// </summary>
        /// <param name="node"></param>
        /// <param name="grandParent"></param>
        /// <param name="currentPath"></param>
        private void TraverseRecursively(XmlNode node, Transform grandParent, string currentPath = "")
        {
            var newPath = currentPath != "" ?  $"{currentPath}/{node.Name}" : node.Name;
            var parent = InstantiateFolder(node.Name, grandParent, newPath);

            foreach (XmlNode childNode in node.ChildNodes)
            {
                InstantiateButton(childNode.Name, parent, newPath);
                TraverseRecursively(childNode, parent, newPath);
            }
        }

        /// <summary>
        /// Instantiates a folder object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private Transform InstantiateFolder(string name, Transform parent, string folderPath)
        {
            var folder = Object.Instantiate(pm.GetPrefabByName(ModelName.FOLDER), parent);
            folder.name = name;
            folder.transform.GetChild(0).name += folderPath;

            return folder.transform;
        }

        /// <summary>
        /// Instantiates a button object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="currentPath"></param>
        private void InstantiateButton(string name, [CanBeNull] Transform parent, string currentPath)
        {
            var button = Object.Instantiate(pm.GetPrefabByName(ModelName.BUTTON), parent);
            var buttonText = button.transform.GetChild(0);
            var rectTransform = button.GetComponent<RectTransform>();
            var targetPath = $"{currentPath}/{name}";

            rectTransform.sizeDelta = new Vector2(150, 50);
            button.name = "to_" + name;
            buttonText.GetComponent<TextMeshProUGUI>().text = name;
            button.transform.GetChild(1).name = targetPath;
        }
    }
}