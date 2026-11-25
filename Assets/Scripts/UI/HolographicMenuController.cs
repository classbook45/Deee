using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PocketRealms.UI
{
    public sealed class HolographicMenuController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform panel;
        [SerializeField] private float entranceDuration = 0.75f;
        [SerializeField] private UnityEvent onPlay;
        [SerializeField] private UnityEvent onCompanion;
        [SerializeField] private UnityEvent onSettings;
        private Vector3 shownScale;

        private void Awake()
        {
            if (panel != null) shownScale = panel.localScale;
            if (canvasGroup != null) canvasGroup.alpha = 0f;
        }
        private void Start() => Show();
        public void Show() => StartCoroutine(AnimateMenu(true));
        public void Hide() => StartCoroutine(AnimateMenu(false));
        public void Play() => onPlay?.Invoke();
        public void OpenCompanion() => onCompanion?.Invoke();
        public void OpenSettings() => onSettings?.Invoke();

        private IEnumerator AnimateMenu(bool show)
        {
            if (canvasGroup == null || panel == null) yield break;
            float elapsed = 0f;
            float fromAlpha = canvasGroup.alpha;
            Vector3 fromScale = panel.localScale;
            Vector3 toScale = show ? shownScale : shownScale * 0.88f;
            float toAlpha = show ? 1f : 0f;
            while (elapsed < entranceDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / entranceDuration);
                float eased = 1f - Mathf.Pow(1f - t, 3f);
                canvasGroup.alpha = Mathf.Lerp(fromAlpha, toAlpha, eased);
                panel.localScale = Vector3.Lerp(fromScale, toScale, eased);
                yield return null;
            }
            canvasGroup.alpha = toAlpha;
            panel.localScale = toScale;
        }
    }
}
