using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
