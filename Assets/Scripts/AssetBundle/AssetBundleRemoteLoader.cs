using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Flux.EvaluationProject
{
    public class AssetBundleRemoteLoader : AbstractAssetBundleLoader
    {
        [SerializeField, Tooltip("The URL where asset bundle is.")]
        private string url;

        protected override IEnumerator InstantiateAsync() =>
            InstantiateAsync(url, prefabName, transform);

        /// <summary>
        /// Loads and instantiates a Prefab from a remote asset bundle.
        /// </summary>
        /// <param name="url">The URL where asset bundle is.</param>
        /// <param name="prefabName">The prefab name inside the asset bundle.</param>
        /// <param name="parent">A transform where the Prefab will be instantiated as a child.</param>
        /// <returns></returns>
        public static IEnumerator InstantiateAsync(string url, string prefabName, Transform parent)
        {
            using var request = UnityWebRequestAssetBundle.GetAssetBundle(url);

            yield return request.SendWebRequest();

            var hasErrors = request.result != UnityWebRequest.Result.Success;
            if (hasErrors)
            {
                Debug.LogErrorFormat("Error on Web Request at {0}, {1}", url, request.error);
                yield break;
            }

            var bundle = DownloadHandlerAssetBundle.GetContent(request);

            if (bundle == null)
            {
                Debug.LogError("Failed to load AssetBundle.");
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
