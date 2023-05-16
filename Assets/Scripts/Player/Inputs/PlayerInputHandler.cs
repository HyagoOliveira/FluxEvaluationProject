using UnityEngine;
using UnityEngine.InputSystem;
using static Flux.EvaluationProject.PlayerInputActions;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Component responsible to receive the Player inputs and forwards them.
    /// </summary>
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMotor motor;
        [SerializeField] private PlayerAnimator animator;

        private PlayerActions actions;

        private void Reset()
        {
            motor = GetComponent<PlayerMotor>();
            animator = GetComponent<PlayerAnimator>();
        }

        private void Awake()
        {
            var input = new PlayerInputActions();
            actions = input.Player;
        }

        private void OnEnable() => actions.Enable();

        private void Update()
        {
            var moveInput = actions.Move.ReadValue<Vector2>();
            var lookInput = actions.Look.ReadValue<Vector2>();
            var jumpInput = IsButtonPressed(actions.Jump);
            var sprintInput = IsButtonPressed(actions.Sprint);
            var punchAttackInput = IsButtonPressed(actions.PunchAttack);
            var kickAttackInput = IsButtonPressed(actions.KickAttack);

            motor.Move(moveInput);
            motor.IsSprinting = sprintInput;

        }

        private void OnDisable() => actions.Disable();

        //Function InputAction.IsPressed() is only available on Input System 1.1.0
        private static bool IsButtonPressed(InputAction button) => button.ReadValue<float>() > 0F;
    }
}
