using UnityEditor;

namespace Flux.EvaluationProject.Editor
{
    /// <summary>
    /// Editor class for <see cref="PlayerColorsData"/>.
    /// <para>
    /// It will find or create a new <see cref="PlayerColorsData"/> asset to be edited.
    /// </para>
    /// </summary>
    public sealed class PlayerColorsDataEditor : EditorWindow
    {
        private static UnityEditor.Editor dataEditor;
        private static readonly string dataName = typeof(PlayerColorsData).Name;

        [MenuItem("Window/Flux Games/Player Colors")]
        private static void ShowWindow()
        {
            var data = FindData();
            var window = GetWindow<PlayerColorsDataEditor>(title: "Player Colors");

            dataEditor = UnityEditor.Editor.CreateEditor(data);

            window.Show();
        }

        private void OnGUI() => dataEditor.OnInspectorGUI();

        private static PlayerColorsData FindData()
        {
            var hasData = EditorBuildSettings.TryGetConfigObject(dataName, out PlayerColorsData data);
            if (!hasData)
            {
                data = CreateDataAsset();
                EditorBuildSettings.AddConfigObject(dataName, data, overwrite: true);
            }

            return data;
        }

        private static PlayerColorsData CreateDataAsset()
        {
            var path = EditorUtility.SaveFilePanelInProject(
                title: "Save " + dataName,
                defaultName: dataName,
                extension: "asset",
                message: "Please select a file or enter a filename to save the Player Colors asset."
            ).Trim();

            var invalidPath = string.IsNullOrEmpty(path);
            if (invalidPath) return null;

            var currentData = AssetDatabase.LoadAssetAtPath<PlayerColorsData>(path);
            var hasCurrentData = currentData != null;

            if (hasCurrentData) return currentData;

            var data = CreateInstance<PlayerColorsData>();

            AssetDatabase.CreateAsset(data, path);
            AssetDatabase.SaveAssets();

            return data;
        }
    }
}
