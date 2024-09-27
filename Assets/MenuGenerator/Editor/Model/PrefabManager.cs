using UnityEditor;
using UnityEngine;

namespace MenuGenerator.Editor.Model
{
    public class PrefabManager
    {
        /// <summary>
        /// Returns a prefab as GameObject by its name. Needs to be in the Prefabs folder.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject GetPrefabByName(string name)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/MenuGenerator/Editor/Model/Prefabs/" + name + ".prefab"
            );
        }
    }
}