using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 5.0f;
    [SerializeField] private Renderer _objectRenderer;

    private void Start()
    {
        Out();
    }

    public void Out()
    {
        foreach (Material material in _objectRenderer.materials)
        {
            StartCoroutine(FadeMaterialOutRoutine(material));
        }
    }

    private IEnumerator FadeMaterialOutRoutine(Material material)
    {
        float elapsedTime = (1 - material.color.a) * _fadeDuration;
        Color startColor = material.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        while (elapsedTime < _fadeDuration)
        {
            float t = elapsedTime / _fadeDuration;
            material.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.color = endColor;
    }
}
