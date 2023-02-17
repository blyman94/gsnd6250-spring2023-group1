using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private GameEvent _logAddSuccessEvent;
    [SerializeField] private GameEvent _logAddFailedEvent;
    [SerializeField] private BoolVariable _playerHasLogVariable;

    public void TryAddLog()
    {
        if (_playerHasLogVariable.Value)
        {
            _playerHasLogVariable.Value = false;
            _logAddSuccessEvent.Raise();
        }
        else
        {
            _logAddFailedEvent.Raise();
        }
    }
}
