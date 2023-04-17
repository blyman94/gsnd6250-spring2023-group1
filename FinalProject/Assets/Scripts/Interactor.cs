using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _interactDistance = 2.0f;
    [SerializeField] private LayerMask _detectLayers;

    private Interactable _interactable;

    private void Update()
    {
        if (Physics.Raycast(transform.position,
            transform.forward, out RaycastHit hitInfo,
            _interactDistance, _detectLayers))
        {
            if (hitInfo.collider.CompareTag("Interactable"))
            {
                _interactable = hitInfo.collider.GetComponent<Interactable>();
                _interactable.StartHover();
            }
        }
        else
        {
            if (_interactable != null)
            {
                _interactable.EndHover();
            }
            _interactable = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position +
            (transform.forward * _interactDistance));
    }

    public void Activate()
    {
        if (_interactable != null)
        {
            _interactable.Interact();
        }
    }
}
