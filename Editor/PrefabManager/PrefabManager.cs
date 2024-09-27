using UnityEditor;
using UnityEngine;

namespace MenuGenerator.Editor.PrefabManager
{
    public class PrefabManager
    {
        public GameObject GetPrefabByName(string name)
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/MenuGenerator/Editor/PrefabManager/Prefabs/" + name + ".prefab"
            );
        }
    }
}