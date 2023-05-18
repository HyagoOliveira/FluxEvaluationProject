using System;
using System.Collections;

namespace Flux.EvaluationProject
{
    public interface IAssetBundleLoader
    {
        /// <summary>
        /// Event triggered when the Asset Bundle loading process is finished.
        /// </summary>
        event Action OnLoadCompleted;

        /// <summary>
        /// Loads the Asset Bundle.
        /// </summary>
        /// <returns></returns>
        IEnumerator Load();
    }
}
