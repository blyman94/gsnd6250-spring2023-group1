using Cinemachine;
using UnityEngine;

/// <summary>
/// Allows the player to switch between first and third person views.
/// </summary>
public class PlayerPOVSelector : MonoBehaviour
{
    [Header("First Person View Components")]
    [SerializeField] private CinemachineVirtualCamera _playerFirstPersonCamera;
    [SerializeField] private SyncYRotationToQuaternionVariable _playerRotationSync;

    [Header("Third Person View Components")]
    [SerializeField] private CinemachineFreeLook _playerThirdPersonCamera;
    [SerializeField] private RotateBasedOnCMFreeLookPosition _playerRotateBased;

    public bool IsInFirstPersonView { get; set; } = true;

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
        _playerFirstPersonCamera.Priority = 0;
        _playerThirdPersonCamera.Priority = 1;
        _playerRotationSync.enabled = false;
        _playerRotateBased.enabled = true;
        IsInFirstPersonView = false;
    }
}
