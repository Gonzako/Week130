using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
public class UIViewsManager : MonoBehaviour
{
    [SerializeField] private UIView _LevelFailView;
    [SerializeField] private UIView _LevelCompleteView;

    private void OnEnable()
    {
        GameManager.onLevelFail += ShowLevelFailView;
    }

    private void OnDisable()
    {
        GameManager.onLevelFail -= ShowLevelFailView;
    }

    private void ShowLevelFailView()
    {
        _LevelFailView.Show();
    }

    private void ShowLevelCompleteView()
    {
        _LevelCompleteView.Show();
    }
}
