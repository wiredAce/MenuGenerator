using System;
using UnityEngine;

namespace MenuGenerator.Behaviours
{
    public class Navigator: MonoBehaviour
    {
        /// <summary>
        /// Action to trigger the deactivation of all menu elements
        /// </summary>
        public event Action AllInactive;

        /// <summary>
        /// Root Object of the menu. Starting point for navigation
        /// </summary>
        public Transform rootTransform;

        /// <summary>
        /// Initialization status
        /// </summary>
        public bool hasInitialized;

        /// <summary>
        /// Starts the initialization of the object
        /// </summary>
        private void Start()
        {
            Init();
        }

        /// <summary>
        /// Finds the root object of the menu
        /// </summary>
        private void Init()
        {
            rootTransform = gameObject.transform.parent;

            hasInitialized = true;
        }

        /// <summary>
        /// Switch to the target layer
        /// </summary>
        /// <param name="path"></param>
        public void JumpToLayer(string path)
        {
            if (!hasInitialized) return;

            AllInactive?.Invoke();
            var pathArray = path.Split('/');
            var lastTransform = rootTransform;
            var i = 0;
            var pathArrayLength = pathArray.Length;

            foreach (var folderName in pathArray)
            {
                var folder = lastTransform.Find(folderName);
                folder.GetComponent<FolderBehaviour>().SwitchActivity(true);
                lastTransform = folder;
                i++;

                if (i != pathArrayLength) continue;

                foreach (Transform child in folder)
                {
                    if (child.TryGetComponent<ButtonBehaviour>(out var button)) button.SwitchActivity(true);
                }
            }
        }
    }
}