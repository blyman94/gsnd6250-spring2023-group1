using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoiceoverSequence : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private float _delayTime;
    [SerializeField] private float[] _timeBetweenClips;
    [SerializeField] private CanvasGroupFader _sceneFader;

    public void StartSequence()
    {
        StartCoroutine(VoiceOverRoutine());
    }

    private IEnumerator VoiceOverRoutine()
    {
        yield return new WaitForSeconds(_delayTime);
        for (int i = 0; i < _clips.Length; i++)
        {
            _audioSource.PlayOneShot(_clips[i]);
            yield return new WaitForSeconds(_timeBetweenClips[i]);
        }

        if (_sceneFader != null)
        {
            yield return _sceneFader.FadeRoutine(true);
            Debug.Log("Switch to new scene!");
        }
    }
}
