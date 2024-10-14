using System.Xml;
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
        private readonly PrefabManager pm = new();

        /// <summary>
        /// Initializes basic structure and starts recursive traversal
        /// </summary>
        /// <param name="document"></param>
        public void Build(XmlDocument document, XmlMeta meta)
        {
            var rootNode = document.FirstChild;

            if (null != rootNode["meta"])
                rootNode.RemoveChild(rootNode["meta"]);

            var rootObject = InitRoot(meta);
            rootObject.name = "root";
            InitNavigator(rootObject).GetComponent<Navigator>();
            TraverseRecursively(rootNode, rootObject, "",true);
        }

        /// <summary>
        /// Initializes the parent object for the menu
        /// </summary>
        /// <returns></returns>
        private Transform InitRoot(XmlMeta meta)
        {
            var rootObject = Object.Instantiate(pm.GetPrefabByName(ModelName.ROOT));
            Object.Instantiate(pm.GetPrefabByName(ModelName.EVENT_SYSTEM), rootObject.transform);

            var canvas = rootObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            if (null != meta && 0 < meta.MenuName.Length) rootObject.name = meta.MenuName;

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
        /// <param name="firstLayer"></param>
        private void TraverseRecursively(XmlNode node, Transform grandParent, string currentPath = "", bool firstLayer = false)
        {
            var newPath = firstLayer ? node.Name : $"{currentPath}/{node.Name}";
            var parent = InstantiateFolder(node.Name, grandParent, newPath, firstLayer);
            var childNodes = node.ChildNodes;
            var childCount = childNodes.Count;

            if (!firstLayer) InstantiateReturnButton(currentPath, parent);

            for (int i = 0; i < childCount; i++)
            {
                var childNode = childNodes[i];
                var offset = (childCount - i - 1) * 100;

                InstantiateButton(childNode.Name, parent, newPath, offset, firstLayer);
                TraverseRecursively(childNode, parent, newPath);
            }
        }

        /// <summary>
        /// Instantiates a folder object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="folderPath"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        private Transform InstantiateFolder(string name, Transform parent, string folderPath, bool isActive)
        {
            var folder = Object.Instantiate(pm.GetPrefabByName(ModelName.FOLDER), parent);
            var src = Object.Instantiate(pm.GetPrefabByName(ModelName.EMPTY), folder.transform);
            folder.name = name;
            folder.transform.GetChild(0).name += folderPath;
            folder.SetActive(isActive);
            src.name = "src";

            return folder.transform;
        }

        /// <summary>
        /// Instantiates a button object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="currentPath"></param>
        /// <param name="offset"></param>
        /// <param name="isActive"></param>
        private void InstantiateButton(string name, Transform parent, string currentPath, int? offset, bool isActive)
        {
            var button = Object.Instantiate(pm.GetPrefabByName(ModelName.BUTTON), parent);
            var buttonText = button.transform.GetChild(0);
            var rectTransform = button.GetComponent<RectTransform>();
            var targetPath = $"{currentPath}/{name}";

            rectTransform.sizeDelta = new Vector2(150, 50);

            if (null != offset) rectTransform.anchoredPosition = new Vector2(0, offset.Value);

            button.name = "to_" + name;
            buttonText.GetComponent<TextMeshProUGUI>().text = name;
            button.transform.GetChild(1).name = targetPath;
            button.SetActive(isActive);
        }

        /// <summary>
        /// Instantiates a button object that when clicked returns one layer up
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="parent"></param>
        private void InstantiateReturnButton(string targetPath, Transform parent)
        {
            var button = Object.Instantiate(pm.GetPrefabByName(ModelName.BUTTON), parent);
            var rectTransform = button.GetComponent<RectTransform>();
            var buttonText = button.transform.GetChild(0);

            rectTransform.sizeDelta = new Vector2(50, 50);
            rectTransform.anchoredPosition = new Vector2(-200, -200);
            buttonText.GetComponent<TextMeshProUGUI>().text = "<-";
            button.transform.GetChild(1).name = targetPath;
            button.name = "re";
        }
    }
}