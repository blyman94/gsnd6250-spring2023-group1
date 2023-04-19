using System.Collections;
using System.Collections.Generic;
using Blyman94.CommonSolutions;
using UnityEngine;

public class PuzzleObjectSlot : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _displayObject;
    
    [Header("Data")]
    [SerializeField] private BoolVariable _hasPieceFlag;
    [SerializeField] private BoolVariable _placedPieceFlag;

    public void PlacePuzzlePiece()
    {
        if (_hasPieceFlag.Value)
        {
            _displayObject.SetActive(true);
            _placedPieceFlag.Value = true;
        }
    }
}
