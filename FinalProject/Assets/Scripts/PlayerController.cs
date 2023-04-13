using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Mover _playerMover;
    [SerializeField] private Interactor _playerInteractor;

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
