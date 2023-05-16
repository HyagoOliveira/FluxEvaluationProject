using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Component responsible to deal only with the Player animations.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int punchId = Animator.StringToHash("Punch");
        private static readonly int kickId = Animator.StringToHash("Kick");
        private static readonly int normalizedSpeedId = Animator.StringToHash("NormalizedSpeed");
        private static readonly int groundedId = Animator.StringToHash("Grounded");
        private static readonly int jumpId = Animator.StringToHash("Jump");

        private void Reset() => animator = GetComponent<Animator>();

        public void SetGrounded(bool isGrounded) => animator.SetBool(groundedId, isGrounded);
        public void SetNormalizedSpeed(float speed) => animator.SetFloat(normalizedSpeedId, speed);

        public void Jump() => animator.SetTrigger(jumpId);
        public void Kick() => animator.SetTrigger(kickId);
        public void Punch() => animator.SetTrigger(punchId);
    }
}
