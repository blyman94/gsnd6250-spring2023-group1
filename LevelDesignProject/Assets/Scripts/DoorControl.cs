using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] private Transform _doorPivot;
    [SerializeField] private float _closedYRotation = -90.0f;
    [SerializeField] private float _openYRotation = -180.0f;
    [SerializeField] private bool _doorOpen = false;
    [SerializeField] private BoolVariable _dependency;
    [SerializeField] private bool _isLocked = false;

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
    }

    public void CloseDoor()
    {
        _doorPivot.localRotation = Quaternion.Euler(0.0f, _closedYRotation, 0.0f);
        _doorOpen = false;
    }
}
