using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Mover _playerMover;
    [SerializeField] private Interactor _playerInteractor;
    [SerializeField] private HandDrum _handDrum;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnActivateInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerInteractor.Activate();
            if (_handDrum.gameObject.activeInHierarchy)
            {
                _handDrum.Strike();
            }
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerMover.MoveInput = context.ReadValue<Vector2>();
        }
        else
        {
            _playerMover.MoveInput = Vector2.zero;
        }
    }
}
