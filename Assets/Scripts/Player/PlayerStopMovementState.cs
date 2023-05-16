using UnityEngine;

namespace Flux.EvaluationProject
{
    public class PlayerStopMovementState : StateMachineBehaviour
    {
        private PlayerMotor motor;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            if (motor == null) motor = animator.GetComponent<PlayerMotor>();

            motor.CanMove = false;
            motor.StopVelocity();
            motor.StopMoveInput();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            motor.CanMove = true;
        }
    }
}
