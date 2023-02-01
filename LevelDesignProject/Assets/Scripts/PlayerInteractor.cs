using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
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
            if (_currentInteractable != null && _currentInteractable.HasLookResponse)
            {
                _currentInteractable.OnLook();
            }
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

    public void Activate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.Interact();
            }
        }
    }
}
