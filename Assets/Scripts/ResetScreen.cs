using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResetScreen : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private BlockWall _blockWall;

    public event UnityAction ResetButtonClicked;

    private void OnEnable()
    {
        _resetButton.onClick.AddListener(ResetLevel);
        _blockWall.AllBlocksStopped += OnAllBlocksStopped;
    }

    private void OnDisable()
    {
        _resetButton.onClick.RemoveListener(ResetLevel);
        _blockWall.AllBlocksStopped -= OnAllBlocksStopped;
    }

    private void ResetLevel()
    {
        _resetButton.gameObject.SetActive(false);
        ResetButtonClicked?.Invoke();
    }

    private void OnAllBlocksStopped()
    {
        _resetButton.gameObject.SetActive(true);
    }
}
