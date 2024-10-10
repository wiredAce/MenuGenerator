using System;
using UnityEngine;

namespace MenuGenerator.Behaviours
{
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
            navigator = FindObjectOfType<Navigator>();
            navigator.AllInactive += () => SwitchActivity(false);
            hasInitialized = true;
        }

        /// <summary>
        /// Switches the activity of the attached gameObject
        /// </summary>
        /// <param name="isActive"></param>
        public void SwitchActivity(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}