using UnityEngine;
using UnityEngine.InputSystem;

public class FreeFlyCameraController : MonoBehaviour
{
    /// <summary>
    /// TransformReference for the free fly camera.
    /// </summary>
    [Tooltip("TransformReference for the free fly camera.")]
    [SerializeField] private TransformReferenceVariable _freeFlyCameraTransformReference;

    /// <summary>
    /// Player input component.
    /// </summary>
    [Tooltip("Player input component.")]
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private PlayerPOVSelector _povSelector;

    /// <summary>
    /// Free fly camera to be controlled.
    /// </summary>
    private FreeFlyCamera _freeFlyCamera;

    #region MonoBehaviour Methods
    private void Start()
    {
        _freeFlyCamera =
            _freeFlyCameraTransformReference.Value.GetComponent<FreeFlyCamera>();
    }
    #endregion 

    #region Input Action Responses
    public void OnSwitchPOVInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _povSelector.SwitchView();
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _freeFlyCamera.IsGamepadInput =
            _playerInput.currentControlScheme == "Gamepad";

        if (context.performed)
        {
            _freeFlyCamera.MoveInput = context.ReadValue<Vector2>();
        }
        else
        {
            _freeFlyCamera.MoveInput = Vector2.zero;
        }
    }

    public void OnRaiseLowerInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _freeFlyCamera.VerticalInput = context.ReadValue<int>();
        }
        else
        {
            _freeFlyCamera.VerticalInput = 0;
        }
    }
    #endregion
}
