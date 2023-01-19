using Cinemachine;
using UnityEngine;

/// <summary>
/// Rotates the character such that it is facing away from an assigned
/// Cinemachine free-look camera.
/// </summary>
public class RotateBasedOnCMFreeLookPosition : MonoBehaviour
{
    /// <summary>
    /// How long it takes for the GameObject to rotate from its current rotation
    /// to a target rotation.
    /// </summary>
    [Tooltip("How long it takes for the GameObject to rotate from its " +
        "current rotation to a target rotation.")]
    [SerializeField] private float rotationSmoothTime = 0.12f;

    /// <summary>
    /// Camera following this GameObject.
    /// </summary>
    [Header("General")]
    [Tooltip("Camera following this GameObject.")]
    [SerializeField] private Transform cameraTransform;

    /// <summary>
    /// Rotation velocity to be updated each time SmoothDampAngle is called.
    /// </summary>
    private float currentYVelocity;

    /// <summary>
    /// Target Y rotation of the GameObject based on movement input.
    /// </summary>
    private float targetRotation;

    #region Properties
    /// <summary>
    /// Vector2 representing the movement input from a control source.
    /// </summary>
    public Vector2 MoveInput { get; set; }
    #endregion

    #region MonoBehaviour Methods
    private void Update()
    {
        if (MoveInput == Vector2.zero)
        {
            return;
        }
        Vector3 inputDirection =
            new Vector3(MoveInput.x, 0.0f, MoveInput.y).normalized;
        targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) *
               Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                targetRotation, ref currentYVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }
    #endregion
}
