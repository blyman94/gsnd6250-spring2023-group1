using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Mover _playerMover;
    [SerializeField] private Interactor _playerInteractor;
    [SerializeField] private HandDrum _handDrum;
    [SerializeField] private Animator _letterAndOpenerAnimator;
    [SerializeField] private Animator _candleLighterAnimator;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnActivateInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_handDrum.gameObject.activeInHierarchy)
            {
                _handDrum.Strike();
            }
            if (_letterAndOpenerAnimator.gameObject.activeInHierarchy)
            {
                _letterAndOpenerAnimator.SetTrigger("Open");
            }
            if (_candleLighterAnimator.gameObject.activeInHierarchy)
            {
                _candleLighterAnimator.SetTrigger("Light");
            }
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
