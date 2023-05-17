using System;
using UnityEngine;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Holds the Player colors for body, arms and legs.
    /// </summary>
    [Serializable]
    public struct PlayerMaterialColors
    {
        [SerializeField, Tooltip("The color used at the Player body.")]
        private Color body;
        [SerializeField, Tooltip("The color used at the Player arms.")]
        private Color arms;
        [SerializeField, Tooltip("The color used at the Player legs.")]
        private Color legs;

        /// <summary>
        /// The color used at the Player body.
        /// </summary>
        public Color Body => body;

        /// <summary>
        /// The color used at the Player arms.
        /// </summary>
        public Color Arms => arms;

        /// <summary>
        /// The color used at the Player legs.
        /// </summary>
        public Color Legs => legs;

        public PlayerMaterialColors(Color color) : this(color, color, color) { }

        public PlayerMaterialColors(Color body, Color arms, Color legs)
        {
            this.body = body;
            this.arms = arms;
            this.legs = legs;
        }
    }
}
