using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] private float _defaultFadeTime = 2.0f;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator Fade(bool fadeIn)
    {
        yield return Fade(fadeIn, _defaultFadeTime);
    }

    public IEnumerator Fade(bool fadeIn, float fadeDuration)
    {
        float elapsedTime = fadeIn ?
            (_canvasGroup.alpha * fadeDuration) :
            (1 - _canvasGroup.alpha) * fadeDuration;
        float startAlpha = _canvasGroup.alpha;
        float endAlpha = fadeIn ? 1.0f : 0.0f;

        while (elapsedTime < fadeDuration)
        {
            _canvasGroup.alpha =
                Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = endAlpha;
    }
}
