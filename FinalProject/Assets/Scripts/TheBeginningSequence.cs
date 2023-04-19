using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;

public class TheBeginningSequence : MonoBehaviour
{
    [SerializeField] private ApplicationManager _appManager;
    [SerializeField] private CanvasGroupFader _sceneFader;
    [SerializeField] private int _sceneToLoad;

    public void StartSequence()
    {
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        _sceneFader.GetComponent<CanvasGroup>().blocksRaycasts = true;
        _sceneFader.GetComponent<CanvasGroup>().interactable = true;
        yield return _sceneFader.FadeRoutine(true);
        _appManager.LoadSceneSingle(_sceneToLoad);
    }
}
