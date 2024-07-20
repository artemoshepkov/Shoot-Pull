using UnityEngine;

namespace Codebase.Player.Rope
{
    public static class AssetProvider
    {
        public static GameObject Instantiate(string path, Vector2 position, Quaternion quaternion, Transform parent)
        {
            Object prefab = Resources.Load(path);
            return Object.Instantiate(prefab, position, quaternion, parent) as GameObject;
            ;
        }
    }
}