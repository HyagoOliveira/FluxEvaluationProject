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
            var rootPath = Directory.GetParent(Application.dataPath).ToString();
            var buildPath = Path.Combine(rootPath, "AssetsBundles");

            try
            {
                if (!Directory.Exists(buildPath)) Directory.CreateDirectory(buildPath);

                BuildPipeline.BuildAssetBundles(
                    buildPath,
                    BuildAssetBundleOptions.None,
                    EditorUserBuildSettings.activeBuildTarget
                );

                Debug.LogFormat("Assets Bundles created at {0}", buildPath);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}
