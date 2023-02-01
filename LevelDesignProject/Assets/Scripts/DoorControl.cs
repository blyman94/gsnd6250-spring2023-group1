using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Transform _doorPivot;
    [SerializeField] private float _closedYRotation = -90.0f;
    [SerializeField] private float _openYRotation = -180.0f;
    [SerializeField] private bool _doorOpen = false;
    [SerializeField] private BoolVariable _dependency;
    [SerializeField] private bool _isLocked = false;
    [SerializeField] private UnityEvent _firstAttemptResponse;
    [SerializeField] private UnityEvent _moreAttemptResponse;
    [SerializeField] private bool _isFirstAttempt = true;

    public void ToggleDoor()
    {
        if (_doorOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (!_isLocked || _dependency.Value)
        {
            _doorPivot.localRotation = Quaternion.Euler(0.0f, _openYRotation, 0.0f);
            _doorOpen = true;
        }
        else
        {
            if (_isFirstAttempt)
            {
                _isFirstAttempt = false;
                _firstAttemptResponse.Invoke();
            }
            else
            {
                _moreAttemptResponse.Invoke();
            }
        }
    }

    public void CloseDoor()
    {
        _doorPivot.localRotation = Quaternion.Euler(0.0f, _closedYRotation, 0.0f);
        _doorOpen = false;
    }
}
