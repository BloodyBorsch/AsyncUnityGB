using UnityEngine;


namespace LessonNine
{
    public abstract class BaseMenuObject : MonoBehaviour, IMenuObject
    {
        private CanvasGroup _canvas;

        public virtual void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            _canvas.alpha = 1.0f;
            _canvas.interactable = true;
            _canvas.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvas.alpha = 0;
            _canvas.interactable = false;
            _canvas.blocksRaycasts = false;
        }
    }
}