using System;
using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Component responsible to deal with the Player physics using a local <see cref="CharacterController"/>.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;

        [Header("Movement")]
        [Tooltip("Move speed of the character in m/s"), Min(0f)]
        public float moveSpeed = 2f;
        [Tooltip("Sprint speed of the character in m/s"), Min(0f)]
        public float sprintSpeed = 6f;
        [Tooltip("Acceleration rate for movement"), Range(0f, 1f)]
        public float moveAcceleration = 0.2f;
        [Tooltip("Acceleration rate for turning"), Range(0.1f, 1f)]
        public float turnAcceleration = 0.6f;

        [Header("Jump")]
        [Tooltip("The height the player can jump"), Min(0f)]
        public float jumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float gravity = -15f;

        [Header("Ground")]
        [Tooltip("Useful for rough ground")]
        public float groundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController"), Min(0f)]
        public float groundedRadius = 0.28f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask groundLayers;

        public event Action OnJump;
        public event Action OnLand;
        public event Action OnPunch;
        public event Action OnKick;

        public bool CanMove { get; set; } = true;
        public bool WasGrounded { get; private set; }
        public bool IsGrounded { get; private set; }
        public bool IsSprinting { get; private set; }
        public bool IsMoveInputting { get; private set; }

        public Vector2 MoveInput { get; private set; }

        public Vector3 Velocity { get; private set; }

        public float VerticalSpeed { get; private set; }

        private Transform mainCamera;
        private Vector3 moveDirection;
        private float currentMoveSpeed;

        private void Reset()
        {
            groundLayers = LayerMask.GetMask("Default");
            controller = GetComponent<CharacterController>();
        }

        private void Awake()
        {
            mainCamera = Camera.main.transform;
            UpdateGroundCollision();
        }

        private void Update()
        {
            UpdateMovement();
            UpdateRotation();
            UpdateGroundCollision();
        }

        public void SetSprintInput(bool isSprinting) => IsSprinting = CanMove && isSprinting;

        public void SetMoveInput(Vector2 input)
        {
            MoveInput = CanMove ? input : Vector2.zero;
            IsMoveInputting = Mathf.Abs(MoveInput.sqrMagnitude) > 0F;
        }

        public void StopMoveInput()
        {
            MoveInput = Vector2.zero;
            IsMoveInputting = false;
        }

        public void StopVelocity() => Velocity = Vector3.zero;

        public void Jump()
        {
            if (!IsAbleToJump()) return;

            // the square root of H * -2 * G = how much velocity needed to reach desired height
            VerticalSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
            OnJump?.Invoke();
        }

        public void Punch()
        {
            StopVelocity();
            OnPunch?.Invoke();
        }

        public void Kick()
        {
            StopVelocity();
            OnKick?.Invoke();
        }

        /// <summary>
        /// The current normalized speed between 0F -> 1F.
        /// </summary>
        /// <returns>A float number between 0F -> 1F.</returns>
        public float GetNormalizedSpeed() => currentMoveSpeed / sprintSpeed;

        private void UpdateMovement()
        {
            UpdateMoveDirection();
            AddGravityIntoVerticalSpeed();
            UpdateCurrentMoveSpeed();

            Velocity = currentMoveSpeed * moveDirection + Vector3.up * VerticalSpeed;

            var velocityPerSecond = Velocity * Time.deltaTime;
            controller.Move(velocityPerSecond);
        }

        private void UpdateMoveDirection()
        {
            moveDirection = IsMoveInputting ?
                GetMoveInputDirectionRelativeToCamera() :
                Vector3.zero;
        }

        private void AddGravityIntoVerticalSpeed()
        {
            const float maxVerticalSpeed = -25F;
            VerticalSpeed += gravity * Time.deltaTime;
            if (VerticalSpeed < maxVerticalSpeed) VerticalSpeed = maxVerticalSpeed;
        }

        private void UpdateCurrentMoveSpeed()
        {
            if (!IsMoveInputting)
            {
                currentMoveSpeed = 0f;
                return;
            }

            var targetSpeed = IsSprinting ? sprintSpeed : moveSpeed;

            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, targetSpeed, moveAcceleration);
            currentMoveSpeed = RoundInto3DecimalPlaces(currentMoveSpeed);
        }

        private void UpdateRotation()
        {
            var currentDirection = transform.position + transform.forward;
            var targetDirection = transform.position + moveDirection;
            var direction = Vector3.MoveTowards(currentDirection, targetDirection, turnAcceleration);

            transform.LookAt(direction);
        }

        private void UpdateGroundCollision()
        {
            var spherePosition = transform.position + Vector3.down * groundedOffset;

            WasGrounded = IsGrounded;
            IsGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);

            var hasLanded = !WasGrounded && IsGrounded;
            if (hasLanded) Land();
        }

        private Vector3 GetMoveInputDirectionRelativeToCamera()
        {
            var right = mainCamera.right;
            right.y = 0f;
            var forward = Vector3.Cross(right, Vector3.up);
            return (right * MoveInput.x + forward * MoveInput.y).normalized;
        }

        private void Land()
        {
            VerticalSpeed = gravity;
            OnLand?.Invoke();
        }

        private bool IsAbleToJump() => IsGrounded;

        private static float RoundInto3DecimalPlaces(float value) => Mathf.Round(value * 1000f) / 1000f;
    }
}
