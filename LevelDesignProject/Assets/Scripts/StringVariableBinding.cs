using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringVariableBinding : MonoBehaviour
{
    [SerializeField] private StringVariable _observedVariable;
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private float _dialogueDisplayTime = 3.0f;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        _observedVariable.VariableUpdated += UpdateDisplay;
    }
    private void Awake()
    {
        _textDisplay.enabled = false;
    }
    private void OnDisable()
    {
        _observedVariable.VariableUpdated -= UpdateDisplay;
    }
    #endregion

    private void UpdateDisplay()
    {
        _textDisplay.text = _observedVariable.Value;
        StartCoroutine(ShowDialogueRoutine());
    }

    private IEnumerator ShowDialogueRoutine()
    {
        _textDisplay.enabled = true;
        yield return new WaitForSeconds(_dialogueDisplayTime);
        _textDisplay.enabled = false;
    }
}
