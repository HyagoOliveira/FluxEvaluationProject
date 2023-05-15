using UnityEngine;

namespace Flux.EvaluationProject
{
    public class ResourcesLoad : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private GameObject prefab;
#endif
        [SerializeField, HideInInspector] private string path;

        void Awake()
        {
            path = prefab.name;
            if (path != "")
                Instantiate(Resources.Load(path), transform);
        }
    }
}