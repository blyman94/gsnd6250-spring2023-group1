using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string _stringToDisplay;
    [SerializeField] private UnityEvent _OnInteractResponse;

    public void Activate()
    {
        _OnInteractResponse?.Invoke();
    }

    public string GetInteractionString()
    {
        return _stringToDisplay;
    }
}
