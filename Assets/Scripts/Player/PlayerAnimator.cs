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
        private static readonly int speedId = Animator.StringToHash("Speed");
        private static readonly int groundedId = Animator.StringToHash("Grounded");
        private static readonly int jumpId = Animator.StringToHash("Jump");
        private static readonly int freeFallId = Animator.StringToHash("FreeFall");
        private static readonly int motionSpeedId = Animator.StringToHash("MotionSpeed");

        private void Reset() => animator = GetComponent<Animator>();

        public void SetJump(bool isJumping) => animator.SetBool(jumpId, isJumping);
        public void SetGrounded(bool isGrounded) => animator.SetBool(groundedId, isGrounded);
        public void SetFreeFall(bool isFreeFall) => animator.SetBool(freeFallId, isFreeFall);

        public void SetSpeed(float speed) => animator.SetFloat(speedId, speed);
        public void SetMotionSpeed(float speed) => animator.SetFloat(motionSpeedId, speed);

        public void Kick() => animator.SetTrigger(kickId);
        public void Punch() => animator.SetTrigger(punchId);
    }
}
