using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Disables the GameObject and enables it only when the Asset Bundle completes the loading process.
    /// </summary>
    public class AssetBundleWaiterActivator : MonoBehaviour
    {
        // A better approach would be fetch this component from some manager.
        // For now, it's serialized directly on the Playground Scene.
        [SerializeField] private AbstractAssetBundleLoader loader;

        private void Awake()
        {
            loader.OnLoadStarted += HandleLoadStarted;
            loader.OnLoadCompleted += HandleLoadCompleted;
        }

        private void OnDestroy()
        {
            loader.OnLoadStarted -= HandleLoadStarted;
            loader.OnLoadCompleted -= HandleLoadCompleted;
        }

        private void HandleLoadStarted() => gameObject.SetActive(false);
        private void HandleLoadCompleted() => gameObject.SetActive(true);
    }
}
