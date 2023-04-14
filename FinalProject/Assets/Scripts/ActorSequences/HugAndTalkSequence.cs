using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TalkType { OneHand, TwoHand }

public class HugAndTalkSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Animator _actorAnimator;

    [Header("Animation Parameters")]
    [SerializeField] private float _hugDuration = 9.0f;
    [SerializeField] private float _talkOneHandDuration = 5.0f;
    [SerializeField] private float _talkTwoHandsDuration = 5.0f;
    [SerializeField] private TalkType[] _talkTypeSequence;

    public void Begin()
    {
        StartCoroutine(HugRoutine());
    }

    private IEnumerator HugRoutine()
    {
        _actorAnimator.SetTrigger("Hug");
        yield return new WaitForSeconds(_hugDuration);
        foreach (TalkType talkType in _talkTypeSequence)
        {
            if (talkType == TalkType.OneHand)
            {
                _actorAnimator.SetTrigger("TalkOneHand");
                yield return new WaitForSeconds(_talkOneHandDuration);
            }
            else
            {
                _actorAnimator.SetTrigger("TalkTwoHands");
                yield return new WaitForSeconds(_talkTwoHandsDuration);
            }
        }

    }
}
