using UnityEngine;
using UnityEngine.UI;

namespace Flux.EvaluationProject
{
    /// <summary>
    /// Simple utility component to counter using a <see cref="Text"/>.
    /// </summary>
    [RequireComponent(typeof(Animation))]
    public class AttackCounter : MonoBehaviour
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        [SerializeField] private Animation animation;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        [SerializeField] private Text text;
        [SerializeField] private string format = "D2";

        public int Counter
        {
            get => counter;
            set
            {
                counter = value;
                text.text = counter.ToString(format);
            }
        }

        private int counter = 0;

        private void Reset()
        {
            animation = GetComponent<Animation>();
            text = GetComponentInChildren<Text>();
        }

        /// <summary>
        /// Increases the Counter and play a default animation.
        /// </summary>
        public void PlayAddAnimation()
        {
            Counter++;
            animation.Play();
        }
    }
}
