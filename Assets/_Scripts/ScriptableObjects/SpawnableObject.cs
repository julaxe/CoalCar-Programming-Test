using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpawnableObject", menuName = "ScriptableObjects/SpawnableObject")]
    public class SpawnableObject : ScriptableObject
    {
        public string id;
        public Sprite sprite;
        public GameObject prefab;
        public GameObject previewPrefab;
    }
}