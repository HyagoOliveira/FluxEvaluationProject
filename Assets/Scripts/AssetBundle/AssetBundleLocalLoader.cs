using UnityEngine;
using System.IO;
using System.Collections;

namespace Flux.EvaluationProject
{
    public class AssetBundleLocalLoader : AbstractAssetBundleLoader
    {
        [SerializeField, Tooltip("The path where the local asset bundle is.")]
        private string localPath;
        [SerializeField, Tooltip("The bundle name.")]
        private string bundleName;
        [SerializeField, Tooltip("The bundle variant.")]
        private string bundleVariant;

        protected override IEnumerator InstantiateAsync() =>
            InstantiateAsync(localPath, bundleName, bundleVariant, prefabName, transform);

        /// <summary>
        /// Loads and instantiates a Prefab from a local asset bundle.
        /// </summary>
        /// <param name="localPath">The path where the local asset bundle is.</param>
        /// <param name="name">The bundle name.</param>
        /// <param name="variant">The bundle variant.</param>
        /// <param name="prefabName">The prefab name inside the asset bundle.</param>
        /// <param name="parent">A transform where the Prefab will be instantiated as a child.</param>
        /// <returns></returns>
        public static IEnumerator InstantiateAsync(
            string localPath,
            string name,
            string variant,
            string prefabName,
            Transform parent
        )
        {
            var bundleName = $"{name}.{variant}";
            var path = Path.Combine(localPath, bundleName);
            var bundleRequest = AssetBundle.LoadFromFileAsync(path);

            yield return bundleRequest;

            var bundle = bundleRequest.assetBundle;
            if (bundle == null)
            {
                Debug.LogErrorFormat("Failed to load AssetBundle {0}", name);
                yield break;
            }

            var prefabRequest = bundle.LoadAssetAsync<GameObject>(prefabName);
            yield return prefabRequest;

            var prefab = prefabRequest.asset as GameObject;
            Instantiate(prefab, parent);

            bundle.Unload(unloadAllLoadedObjects: false);
        }
    }
}
