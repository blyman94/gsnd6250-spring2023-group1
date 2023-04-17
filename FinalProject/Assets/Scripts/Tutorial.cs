using System.Collections;
using System.Collections.Generic;
using TMPro;
using Blyman94.CommonSolutions;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _lookLeftObject;
    [SerializeField] private GameObject _lookRightObject;
    [SerializeField] private GameObject _lookForwardObject;
    [SerializeField] private PlayerController _playerController;

    [Header("UI Component References")]
    [SerializeField] private CanvasGroupFader _sceneFader;
    [SerializeField] private CanvasGroupFader _mousePromptFader;
    [SerializeField] private CanvasGroupFader _movePromptFader;
    [SerializeField] private CanvasGroupFader _clickPromptFader;
    [SerializeField] private Animator _uiMouseAnimator;
    [SerializeField] private TextMeshProUGUI _promptText;

    [Header("Data")]
    [SerializeField] private BoolVariable _hasLookedLeft;
    [SerializeField] private BoolVariable _hasLookedRight;
    [SerializeField] private BoolVariable _hasLookedForward;
    [SerializeField] private BoolVariable _hasWalkedForward;

    private void Awake()
    {
        _hasLookedLeft.Value = false;
        _hasLookedRight.Value = false;
        _hasWalkedForward.Value = false;

        _lookRightObject.SetActive(false);
        _lookForwardObject.SetActive(false);
    }

    private IEnumerator Start()
    {
        _playerController.CanMove = false;
        _playerController.CinemachineInputProvider.enabled = false;
        yield return _sceneFader.Fade(false);
        yield return FadeInAndShowLeftPromptRoutine();
    }

    public void FinishTutorial()
    {
        _playerController.CanMove = false;
        StartCoroutine(TutorialFinishedRoutine());
    }

    public IEnumerator TutorialFinishedRoutine()
    {
        yield return _clickPromptFader.Fade(false, 1.0f);
        yield return _sceneFader.Fade(true);
        Debug.Log("Switch to new scene!");
    }

    private void OnEnable()
    {
        _hasLookedLeft.ValueUpdated += CheckLookLeft;
        _hasLookedRight.ValueUpdated += CheckLookRight;
        _hasLookedForward.ValueUpdated += CheckLookForward;
        _hasWalkedForward.ValueUpdated += CheckWalkedForward;
    }

    private void OnDisable()
    {
        _hasLookedLeft.ValueUpdated -= CheckLookLeft;
        _hasLookedRight.ValueUpdated -= CheckLookRight;
        _hasLookedForward.ValueUpdated -= CheckLookForward;
        _hasWalkedForward.ValueUpdated -= CheckWalkedForward;
    }

    private void CheckLookLeft()
    {
        if (_hasLookedLeft.Value)
        {
            _lookLeftObject.SetActive(false);
            _lookRightObject.SetActive(true);
            StartCoroutine(FadeOutAndSwitchPromptToRightRoutine());
        }
    }

    private void CheckLookRight()
    {
        if (_hasLookedRight.Value)
        {
            _lookRightObject.SetActive(false);
            _lookForwardObject.SetActive(true);
            StartCoroutine(FadeOutAndSwitchPromptToForwardRoutine());
        }
    }

    private void CheckLookForward()
    {
        if (_hasLookedForward.Value)
        {
            _lookForwardObject.SetActive(false);
            StartCoroutine(FadeOutAndSwitchPromptToWalkRoutine());
        }
    }

    private void CheckWalkedForward()
    {
        if (_hasWalkedForward.Value)
        {
            StartCoroutine(FadeOutAndSwitchPromptToClickRoutine());
        }
    }

    private IEnumerator FadeInAndShowLeftPromptRoutine()
    {
        _playerController.CinemachineInputProvider.enabled = true;
        _promptText.text = "Look Left";
        _uiMouseAnimator.SetTrigger("MouseLeft");
        yield return _mousePromptFader.Fade(true, 1.0f);
    }

    private IEnumerator FadeOutAndSwitchPromptToRightRoutine()
    {
        yield return _mousePromptFader.Fade(false, 1.0f);
        _promptText.text = "Look Right";
        _uiMouseAnimator.SetTrigger("MouseRight");
        yield return _mousePromptFader.Fade(true, 1.0f);
    }

    private IEnumerator FadeOutAndSwitchPromptToForwardRoutine()
    {
        yield return _mousePromptFader.Fade(false, 1.0f);
        _promptText.text = "Look Forward";
        _uiMouseAnimator.SetTrigger("MouseLeft");
        yield return _mousePromptFader.Fade(true, 1.0f);
    }

    private IEnumerator FadeOutAndSwitchPromptToWalkRoutine()
    {
        yield return _mousePromptFader.Fade(false, 1.0f);
        yield return new WaitForSeconds(0.5f);
        _playerController.CanMove = true;
        yield return _movePromptFader.Fade(true, 1.0f);
    }

    private IEnumerator FadeOutAndSwitchPromptToClickRoutine()
    {
        yield return _movePromptFader.Fade(false, 1.0f);
        yield return new WaitForSeconds(0.5f);
        yield return _clickPromptFader.Fade(true, 1.0f);
    }

}
