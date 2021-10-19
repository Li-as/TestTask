using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelResetter : MonoBehaviour
{
    [SerializeField] private ResetScreen _resetScreen;
    [SerializeField] private BlockWall _blockWall;

    private bool _isResetting;

    public bool IsResetting => _isResetting;
    public event UnityAction ResetStarted;

    private void OnEnable()
    {
        _resetScreen.ResetButtonClicked += OnResetButtonClicked;
        _blockWall.BlockWallResetFinished += OnBlockWallResetFinished;
    }

    private void OnDisable()
    {
        _resetScreen.ResetButtonClicked -= OnResetButtonClicked;
        _blockWall.BlockWallResetFinished -= OnBlockWallResetFinished;
    }

    private void OnResetButtonClicked()
    {
        _isResetting = true;
        ResetStarted?.Invoke();
        _blockWall.Reset();
    }

    private void OnBlockWallResetFinished()
    {
        _isResetting = false;
    }
}
