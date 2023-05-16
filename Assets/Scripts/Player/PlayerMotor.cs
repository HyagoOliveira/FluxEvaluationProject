using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Component responsible to deal only with the Player physics using a local <see cref="CharacterController"/>.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;

        [Header("Movement")]
        [Tooltip("Move speed of the character in m/s"), Min(0f)]
        public float moveSpeed = 2.0f;
        [Tooltip("Sprint speed of the character in m/s"), Min(0f)]
        public float sprintSpeed = 5.335f;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float rotationSmoothTime = 0.12f;
        [Tooltip("Acceleration and deceleration"), Min(0f)]
        public float speedChangeRate = 10.0f;

        [Header("Jump")]
        [Tooltip("The height the player can jump"), Min(0f)]
        public float jumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float gravity = -15.0f;

        [Header("Ground")]
        [Tooltip("Useful for rough ground")]
        public float groundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController"), Min(0f)]
        public float groundedRadius = 0.28f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask groundLayers;

        /// <summary>
        /// If the character is grounded or not. Not part of the CharacterController built in grounded check.
        /// </summary>
        public bool IsGrounded { get; private set; }

        public bool IsSprinting { get; set; }

        public float Speed { get; private set; }

        private Camera mainCamera;
        private float targetRotation;
        private float rotationVelocity;
        private float verticalSpeed;

        private const float terminalVelocity = 53F;
        private const float groundedVerticalSpeed = -2F;

        private void Reset() => controller = GetComponent<CharacterController>();

        private void Awake() => mainCamera = Camera.main;

        private void Update()
        {
            CheckGround();
            UpdateVerticalSpeed();
        }

        public void Move(Vector2 input)
        {
            bool hasInput = Mathf.Abs(input.sqrMagnitude) > 0f;
            float targetSpeed = hasInput ? GetMoveSpeed() : 0F;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = input.magnitude;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                Speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);

                // round speed to 3 decimal places
                Speed = Mathf.Round(Speed * 1000f) / 1000f;
            }
            else
            {
                Speed = targetSpeed;
            }

            //animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

            // normalise input direction
            Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y).normalized;

            if (hasInput)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = (Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward).normalized;

            // move the player
            var speedPerSeconds = Speed * Time.deltaTime;
            var verticalVelocityPerSeconds = Vector3.up * verticalSpeed * Time.deltaTime;
            var velocity = targetDirection * speedPerSeconds + verticalVelocityPerSeconds;

            controller.Move(velocity);
        }

        private void CheckGround()
        {
            // set sphere position, with offset
            var spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            IsGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        }

        private void UpdateVerticalSpeed()
        {
            if (IsGrounded)
            {
                // stop our velocity dropping infinitely when grounded
                if (verticalSpeed < 0.0f)
                {
                    verticalSpeed = groundedVerticalSpeed;
                }
            }
            else if (verticalSpeed < terminalVelocity)
            {
                verticalSpeed += gravity * Time.deltaTime;
            }
        }

        private void Jump()
        {
            // the square root of H * -2 * G = how much velocity needed to reach desired height
            verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        private float GetMoveSpeed() => IsSprinting ? sprintSpeed : moveSpeed;
    }
}
