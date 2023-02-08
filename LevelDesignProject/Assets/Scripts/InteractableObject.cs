using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] UnityEvent OnInteractResponse;
    [SerializeField] UnityEvent OnLookResponse;

    public bool activated = false;

    public bool HasLookResponse = false;

    public void OnLook()
    {
        OnLookResponse.Invoke();
    }

    public void Interact()
    {
        activated = true;
        OnInteractResponse.Invoke();
    }
}
