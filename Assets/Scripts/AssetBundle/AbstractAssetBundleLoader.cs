using System;
using System.Collections;
using UnityEngine;

namespace Flux.EvaluationProject
{
    public abstract class AbstractAssetBundleLoader : MonoBehaviour
    {
        [SerializeField, Tooltip("Whether to start the loading process when game starts.")]
        private bool loadOnStart = true;
        [SerializeField, Tooltip("The prefab name inside the asset bundle.")]
        protected string prefabName;

        /// <summary>
        /// Event triggered when the Asset Bundle loading process is finished.
        /// </summary>
        public event Action OnLoadCompleted;

        private IEnumerator Start()
        {
            if (loadOnStart) yield return Load();
        }

        /// <summary>
        /// Loads the Asset Bundle.
        /// </summary>
        /// <returns></returns>
        public IEnumerator Load()
        {
            yield return InstantiateAsync();
            OnLoadCompleted?.Invoke();
        }

        protected abstract IEnumerator InstantiateAsync();
    }
}
