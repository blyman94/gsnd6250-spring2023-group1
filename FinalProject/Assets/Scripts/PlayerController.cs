using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Mover _playerMover;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
