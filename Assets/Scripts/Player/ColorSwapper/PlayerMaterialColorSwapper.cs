using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Component to swap the Players color using the local <see cref="colors"/> asset.
    /// </summary>
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class PlayerMaterialColorSwapper : MonoBehaviour
    {
        [SerializeField, Tooltip("The local MeshRenderer component.")]
        private SkinnedMeshRenderer meshRenderer;
        [SerializeField, Tooltip("The color container used to select a random color.")]
        private PlayerColorsData colors;

        private const uint bodyIndex = 0;
        private const uint armsIndex = 1;
        private const uint legsIndex = 2;

        private Material BodyMaterial => meshRenderer.materials[bodyIndex];
        private Material ArmsMaterial => meshRenderer.materials[armsIndex];
        private Material LegsMaterial => meshRenderer.materials[legsIndex];

        private void Reset() => meshRenderer = GetComponent<SkinnedMeshRenderer>();

        private void Awake() => SwapToRandomColor();

        private void SwapToRandomColor()
        {
            var randomColor = colors.GetRandomColor();

            BodyMaterial.color = randomColor.Body;
            ArmsMaterial.color = randomColor.Arms;
            LegsMaterial.color = randomColor.Legs;
        }
    }
}
