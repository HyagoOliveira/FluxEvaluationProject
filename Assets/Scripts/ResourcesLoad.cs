using UnityEngine;

namespace Flux.EvaluationProject
{
    public class ResourcesLoad : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;

        void Awake()
        {
            if (prefab == null) return;

            var path = prefab.name;
            Instantiate(Resources.Load(path), transform);
        }
    }
}