using System;
using UnityEngine;

namespace MenuGenerator.Behaviours
{
    public class FolderBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Own menu layer
        /// </summary>
        private string path { get; set; }

        /// <summary>
        /// Navigator that is responsible for this menu
        /// </summary>
        private Navigator navigator { get; set; }

        /// <summary>
        /// Triggers the initialization of the object
        /// </summary>
        private void Start()
        {
            Init();
        }

        /// <summary>
        /// Collects information from child parameters and subscribes to events
        /// </summary>
        private void Init()
        {
            // Set the parameters
            navigator = SearchNavigator();
            path = gameObject.transform.GetChild(0).name;
            path = path[12..]; //cut identifier => '$filePath:/'
            // Subscribe to events
            navigator.AllInactive += () => SwitchActivity(false);
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
                    if (child.GetComponent<Navigator>() != null)
                    {
                        return child.GetComponent<Navigator>();
                    }
                }

                parent = parent.parent;
            }

            return null;
        }

        /// <summary>
        /// Switches the gameObjects activity
        /// </summary>
        /// <param name="isActive"></param>
        public void SwitchActivity(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}