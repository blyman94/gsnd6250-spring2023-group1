using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _interactDistance = 5.0f;
    [SerializeField] private LayerMask _layermask;

    private InteractableObject _currentInteractable;

    #region MonoBehaviour Methods
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward,
            out RaycastHit hitInfo, _interactDistance, _layermask))
        {
            _currentInteractable =
                hitInfo.collider.transform.GetComponentInParent<InteractableObject>();
        }
        else
        {
            _currentInteractable = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position,
            transform.position + (transform.forward * _interactDistance));
    }
    #endregion

    public void Activate()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }
}
