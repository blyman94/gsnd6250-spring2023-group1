using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryRoomFadeSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private MemoryRoom _memoryRoom;

    [Header("Fade Parameters")]
    [SerializeField] private float[] _priorityFadeDurations;
    [SerializeField] private float _startDelay = 0.0f;
    [SerializeField] private float _timeBetweenObjectFades = 3.0f;

    private int currentPriority;

    private void Start()
    {
        currentPriority = _memoryRoom.MinPriority;
        InvokeRepeating("FadeOutObject", _startDelay, _timeBetweenObjectFades);
    }

    private void FadeOutObject()
    {
        bool priorityHasObjects =
            _memoryRoom.FadeOutRandomObjectInPriority(currentPriority, 1.0f);
        if (!priorityHasObjects)
        {
            if (currentPriority == _memoryRoom.MaxPriority)
            {
                // TODO: End the game here.
                CancelInvoke();
                Debug.Log("Game Over!");
            }
            else
            {
                currentPriority++;
            }
        }
    }
}
