using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseScreen<T> : MonoBehaviour where T : Component
    {
        public static T instance;
        protected CanvasGroup canvasGroup;

        protected virtual void Awake()
        {
            instance = GetComponent<T>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void ShowScreen()
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        public virtual void HideScreen()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }
    }
}
