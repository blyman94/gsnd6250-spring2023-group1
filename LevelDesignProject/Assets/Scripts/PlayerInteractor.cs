using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _interactDistance = 2.0f;
    [SerializeField] private LayerMask _detectLayers;
    [SerializeField] private GameEvent _pickupDetectedEvent;
    [SerializeField] private GameEvent _nothingDetectedEvent;
    [SerializeField] private StringVariable _pickupPromptString;

    private InteractableObject _currentInteractableObject;

    private void Update()
    {
        if (Physics.Raycast(transform.position,
            transform.forward, out RaycastHit hitInfo,
            _interactDistance, _detectLayers))
        {
            if (_currentInteractableObject == null)
            {
                _currentInteractableObject = hitInfo.collider.GetComponent<InteractableObject>();
            }

            if (hitInfo.collider.CompareTag("Interactable"))
            {
                if (!_currentInteractableObject.IsTimeActivated)
                {
                    _pickupDetectedEvent.Raise();
                    _pickupPromptString.Value =
                        string.Format("Interact \n{0}",
                        _currentInteractableObject.GetInteractionString());
                }
                else
                {
                    _currentInteractableObject.IsBeingLookedAt = true;
                }
            }
        }
        else
        {
            _nothingDetectedEvent.Raise();
            if (_currentInteractableObject != null)
            {
                _currentInteractableObject.IsBeingLookedAt = false;
            }
            _currentInteractableObject = null;
        }
    }

    public void Interact()
    {
        if (_currentInteractableObject != null)
        {
            _currentInteractableObject.Activate();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * _interactDistance));
    }
}
