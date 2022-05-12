using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseSingleton<UIManager>
{
    [SerializeField] private GameObject SwipeToStartUI;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject DrawingUI;
    [SerializeField] private GameObject LevelEndUI;
    [SerializeField] private GameObject LevelFailUI;

    private void Awake()
    {
        EventManager.OnStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnStateChanged -= GameStateChanged;
    }

    private void GameStateChanged(GameState state)
    {
        SwipeToStartUI.SetActive(state == GameState.SwipeToStart);
        InGameUI.SetActive(state == GameState.InGame);
        DrawingUI.SetActive(state == GameState.Drawing);
        LevelEndUI.SetActive(state == GameState.LevelEnd);
        LevelFailUI.SetActive(state == GameState.LevelFail);
    }
}
