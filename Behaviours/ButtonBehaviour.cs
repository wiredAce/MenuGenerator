using System;
using UnityEngine;

namespace MenuGenerator.Behaviours
{
    /// <summary>
    /// Behaviour of a menu-button that jumps to a target layer and can be hidden via navigator-events
    /// </summary>
    public class ButtonBehaviour: MonoBehaviour
    {
        /// <summary>
        /// Path of the target layer
        /// </summary>
        private string targetPath { get; set; }

        /// <summary>
        /// Navigator that is responsible for this menu
        /// </summary>
        private Navigator navigator { get; set; }

        /// <summary>
        /// Initialization status
        /// </summary>
        private bool hasInitialized;

        /// <summary>
        /// Triggers the initialization of the object
        /// </summary>
        private void Start()
        {
            Init();
        }

        /// <summary>
        /// Receives the click event from the TMP-Button and jumps to the target layer
        /// </summary>
        public void OnClick()
        {
            if (! hasInitialized) return;

            navigator.JumpToLayer(targetPath);
        }

        /// <summary>
        /// Collects information from child parameters and subscribes to events
        /// </summary>
        private void Init()
        {
            targetPath = gameObject.transform.GetChild(1).name;
            navigator = SearchNavigator();
            navigator.AllInactive += () => SwitchActivity(false);
            hasInitialized = true;
        }

        /// <summary>
        /// Searches for the navigator in the parent hierarchy
        /// </summary>
        /// <returns></returns>
        private Navigator SearchNavigator()
        {
            var parent = gameObject.transform.parent;

            while (parent != null)
            {
                foreach (Transform child in parent)
                {
                    if (child.GetComponent<Navigator>() != null) return child.GetComponent<Navigator>();
                }

                parent = parent.parent;
            }

            return null;
        }

        /// <summary>
        /// Switches the activity of the attached gameObject and the source folder of the corresponding layer
        /// </summary>
        /// <param name="isActive"></param>
        public void SwitchActivity(bool isActive)
        {
            var self = gameObject;
            var src = self.transform.parent.Find("src");

            self.SetActive(isActive);
            if (null != src) src.gameObject.SetActive(isActive);
        }
    }
}