using UnityEngine;
using UnityEngine.InputSystem;
using static Flux.EvaluationProject.PlayerInputActions;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Component responsible to receive the inputs and forward them into <see cref="PlayerMotor"/>.
    /// </summary>
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMotor motor;

        private PlayerActions actions;

        private void Reset() => motor = GetComponent<PlayerMotor>();

        private void Awake()
        {
            var input = new PlayerInputActions();
            actions = input.Player;
        }

        private void Update()
        {
            var moveInput = actions.Move.ReadValue<Vector2>();
            motor.SetMoveInput(moveInput);
        }

        private void OnEnable()
        {
            actions.Jump.started += HandleJumpStarted;
            actions.Jump.canceled += HandleJumpCanceled;

            actions.PunchAttack.started += HandlePunchAttackStarted;
            actions.KickAttack.started += HandleKickAttackStarted;

            actions.Sprint.started += HandleSprintStarted;
            actions.Sprint.canceled += HandleSprintCanceled;

            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();

            actions.Jump.started -= HandleJumpStarted;
            actions.Jump.canceled -= HandleJumpCanceled;

            actions.PunchAttack.started -= HandlePunchAttackStarted;
            actions.KickAttack.started -= HandleKickAttackStarted;

            actions.Sprint.started -= HandleSprintStarted;
            actions.Sprint.canceled -= HandleSprintCanceled;
        }

        private void HandleMoveCanceled(InputAction.CallbackContext _) =>
            motor.SetMoveInput(Vector2.zero);

        private void HandleJumpStarted(InputAction.CallbackContext _) => motor.Jump();
        private void HandleJumpCanceled(InputAction.CallbackContext _) => motor.CancelJump();

        private void HandlePunchAttackStarted(InputAction.CallbackContext _) => motor.Punch();
        private void HandleKickAttackStarted(InputAction.CallbackContext _) => motor.Kick();

        private void HandleSprintStarted(InputAction.CallbackContext _) => motor.SetSprintInput(true);
        private void HandleSprintCanceled(InputAction.CallbackContext _) => motor.SetSprintInput(false);
    }
}
