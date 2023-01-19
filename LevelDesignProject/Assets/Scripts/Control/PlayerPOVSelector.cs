using Cinemachine;
using UnityEngine;

/// <summary>
/// Allows the player to switch between first and third person views.
/// </summary>
public class PlayerPOVSelector : MonoBehaviour
{
    /// <summary>
    /// The player's animator component.
    /// </summary>
    [Header("Component References")]
    [Tooltip("The player's animator component.")]
    [SerializeField] private Animator _playerAnimator;

    /// <summary>
    /// TransformReference for the first person camera.
    /// </summary>
    [Header("First Person View Components")]
    [Tooltip("TransformReference for the first person camera.")]
    [SerializeField] private TransformReferenceVariable _playerFirstPersonCameraTransform;

    /// <summary>
    /// Player character's rotation handler when in first person.
    /// </summary>
    [Tooltip("Player character's rotation handler when in first person.")]
    [SerializeField] private SyncYRotationToQuaternionVariable _playerRotationSync;

    /// <summary>
    /// TransformReference for the first person camera.
    /// </summary>
    [Header("Third Person View Components")]
    [Tooltip("TransformReference for the third person camera.")]
    [SerializeField] private TransformReferenceVariable _playerThirdPersonCameraTransform;

    /// <summary>
    /// Player character's rotation handler when in third person.
    /// </summary>
    [Tooltip("Player character's rotation handler when in thrid person.")]
    [SerializeField] private RotateBasedOnMainCameraRotation _playerRotateBased;

    /// <summary>
    /// Is the player currently in first person view?
    /// </summary>
    public bool IsInFirstPersonView { get; set; } = true;

    /// <summary>
    /// Virtual camera component for the first person camera.
    /// </summary>
    private CinemachineVirtualCamera _playerFirstPersonCamera;

    /// <summary>
    /// Free look camera component for the third person camera.
    /// </summary>
    private CinemachineFreeLook _playerThirdPersonCamera;

    /// <summary>
    /// Parameter ID for the IsFirstPerson string.
    /// </summary>
    private int _isFirstPersonID;

    #region MonoBehaviour Methods
    private void Awake()
    {
        _isFirstPersonID = Animator.StringToHash("IsFirstPerson");
        _playerAnimator.SetBool(_isFirstPersonID, true);
    }
    private void Start()
    {
        _playerFirstPersonCamera =
            _playerFirstPersonCameraTransform.Value.GetComponent<CinemachineVirtualCamera>();
        _playerThirdPersonCamera =
            _playerThirdPersonCameraTransform.Value.GetComponent<CinemachineFreeLook>();
    }
    #endregion

    /// <summary>
    /// Toggles the player's perspective between first and third person.
    /// </summary>
    public void SwitchView()
    {
        if (IsInFirstPersonView)
        {
            SwitchToThirdPersonView();
        }
        else
        {
            SwitchToFirstPersonView();
        }
    }

    /// <summary>
    /// Switches the player's perspective to first person view.
    /// </summary>
    public void SwitchToFirstPersonView()
    {
        _playerAnimator.SetBool(_isFirstPersonID, true);
        _playerFirstPersonCamera.Priority = 1;
        _playerThirdPersonCamera.Priority = 0;
        _playerRotationSync.enabled = true;
        _playerRotateBased.enabled = false;
        IsInFirstPersonView = true;
    }

    /// <summary>
    /// Switches the player's perspective to first person view.
    /// </summary>
    public void SwitchToThirdPersonView()
    {
        _playerAnimator.SetBool(_isFirstPersonID, false);
        _playerFirstPersonCamera.Priority = 0;
        _playerThirdPersonCamera.Priority = 1;
        _playerRotationSync.enabled = false;
        _playerRotateBased.enabled = true;
        IsInFirstPersonView = false;
    }
}
