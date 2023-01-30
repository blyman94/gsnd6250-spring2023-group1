using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] UnityEvent OnInteractResponse;

    public void Interact()
    {
        OnInteractResponse.Invoke();
    }
}
