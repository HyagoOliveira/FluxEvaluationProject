using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Flux.EvaluationProject
{
    public class UIVirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {

        [Header("Output")]
        public UnityEvent<bool> buttonStateOutputEvent;
        public UnityEvent buttonClickOutputEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            OutputButtonStateValue(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OutputButtonStateValue(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OutputButtonClickEvent();
        }

        void OutputButtonStateValue(bool buttonState)
        {
            buttonStateOutputEvent.Invoke(buttonState);
        }

        void OutputButtonClickEvent()
        {
            buttonClickOutputEvent.Invoke();
        }
    }
}
