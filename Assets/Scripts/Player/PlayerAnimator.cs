using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Component responsible to deal only with the Player animations.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerMotor motor;

        private static readonly int punchId = Animator.StringToHash("Punch");
        private static readonly int kickId = Animator.StringToHash("Kick");
        private static readonly int normalizedSpeedId = Animator.StringToHash("NormalizedSpeed");
        private static readonly int groundedId = Animator.StringToHash("Grounded");
        private static readonly int jumpId = Animator.StringToHash("Jump");

        private void Reset()
        {
            animator = GetComponent<Animator>();
            motor = GetComponent<PlayerMotor>();
        }

        private void Start() => SetGrounded(motor.IsGrounded);

        private void Update()
        {
            SetGrounded(motor.IsGrounded);
            SetNormalizedSpeed(motor.GetNormalizedSpeed());
        }

        private void OnEnable()
        {
            motor.OnJump += Jump;
            motor.OnKick += Kick;
            motor.OnPunch += Punch;
        }

        private void OnDisable()
        {
            motor.OnJump -= Jump;
            motor.OnKick -= Kick;
            motor.OnPunch -= Punch;
        }

        public void SetGrounded(bool isGrounded) => animator.SetBool(groundedId, isGrounded);
        public void SetNormalizedSpeed(float speed) => animator.SetFloat(normalizedSpeedId, speed);

        public void Jump() => animator.SetTrigger(jumpId);
        public void Kick() => animator.SetTrigger(kickId);
        public void Punch() => animator.SetTrigger(punchId);
    }
}
