using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Data Container to get a random color from the local <see cref="colors"/> array.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerColorsData", menuName = "Flux Games/Player Colors Data", order = 110)]
    public sealed class PlayerColorsData : ScriptableObject
    {
        [SerializeField, Tooltip("The list of possible colors to use at runtime.")]
        private PlayerMaterialColors[] colors = new PlayerMaterialColors[1]
        {
            new PlayerMaterialColors(Color.white)
        };

        /// <summary>
        /// Gets a random Player color set using <see cref="colors"/> array.
        /// </summary>
        /// <returns>
        /// A <see cref="PlayerMaterialColors"/> instance or a default one if <see cref="colors"/> is empty.
        /// </returns>
        public PlayerMaterialColors GetRandomColor()
        {
            if (colors.Length == 0)
            {
                Debug.LogError("Colors array is empty.");
                return default;
            }

            var randomIndex = Random.Range(0, colors.Length);
            return colors[randomIndex];
        }
    }
}
