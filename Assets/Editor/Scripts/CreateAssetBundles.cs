using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Flux.EvaluationProject.Editor
{
    public static class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        private static void BuildAllAssetBundles()
        {
            var directory = Path.Combine(Application.dataPath, "..", "AssetBundles");

            try
            {
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                BuildPipeline.BuildAssetBundles(
                    directory,
                    BuildAssetBundleOptions.None,
                    EditorUserBuildSettings.activeBuildTarget
                );
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}
