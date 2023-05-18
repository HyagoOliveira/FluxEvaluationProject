using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// UI Manager for displaying the Player attacks amount.
    /// </summary>
    public class PlayerAttackCounterManager : MonoBehaviour
    {
        [SerializeField] private AttackCounter kickCounter;
        [SerializeField] private AttackCounter punchCounter;

        private PlayerMotor player;

        private void Awake()
        {
            // In a production scenario, the best approach would be something like a
            // PlayerManager.OnReady event where other managers could register listeners
            // as soon as the Player is completely ready.

            player = FindObjectOfType<PlayerMotor>(includeInactive: true);

            if (player == null) Debug.LogError("Player is not instantiated in Scene.");
        }

        private void OnEnable()
        {
            if (player == null) return;

            player.OnKick += kickCounter.PlayAddAnimation;
            player.OnPunch += punchCounter.PlayAddAnimation;
        }

        private void OnDisable()
        {
            if (player == null) return;

            player.OnKick -= kickCounter.PlayAddAnimation;
            player.OnPunch -= punchCounter.PlayAddAnimation;
        }
    }
}
