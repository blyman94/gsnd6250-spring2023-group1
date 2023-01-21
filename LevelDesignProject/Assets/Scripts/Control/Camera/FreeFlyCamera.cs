using UnityEngine;

/// <summary>
/// Camera controlled directly for level exploration.
/// </summary>
public class FreeFlyCamera : MonoBehaviour
{
    /// <summary>
    /// Speed at which the camera moves;
    /// </summary>
    [SerializeField] private float MoveSpeed;

    /// <summary>
    /// Is the player currently controlling this camera?
    /// </summary>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// Move input received from control class.
    /// </summary>
    public Vector2 MoveInput { get; set; }

    /// <summary>
    /// Vertical move input received from control class.
    /// </summary>
    public int VerticalInput { get; set; }

    /// <summary>
    /// Determines if the character is receiving input from a gamepad.
    /// </summary>
    public bool IsGamepadInput { get; set; }

    private Vector3 xzMovement;

    private Vector3 yMovement;

    /// <summary>
    /// CharacterController with which the camera will be controlled.
    /// </summary>
    [Tooltip("CharacterController with which the camera will be controlled.")]
    [SerializeField] private CharacterController _characterController;

    #region MonoBehaviour Methods
    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        HandleXZMovement();
        HandleYMovement();
        Vector3 finalMovement = xzMovement + yMovement;
        _characterController.Move(finalMovement * Time.deltaTime);
    }
    #endregion

    /// <summary>
    /// Calculates the new movement speed of the character based on its walk 
    /// speed and acceleration parameters, updating the x and z components of 
    /// the final movement vector.
    /// </summary>
    private void HandleXZMovement()
    {
        if (IsGamepadInput)
        {
            // Gamepad input is analog - character should always be running, 
            // and the move input itself will control the character's speed.
            xzMovement = ((transform.forward * MoveInput.y) +
                (transform.right * MoveInput.x)) * MoveSpeed;
        }
        else
        {
            xzMovement = ((transform.forward * MoveInput.y) +
                (transform.right * MoveInput.x)).normalized *
                MoveSpeed;
        }
    }

    private void HandleYMovement()
    {
        yMovement = (transform.up * VerticalInput).normalized * MoveSpeed;
    }
}
