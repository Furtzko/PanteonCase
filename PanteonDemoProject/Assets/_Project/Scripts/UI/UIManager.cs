using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseSingleton<UIManager>
{
    [SerializeField] private GameObject SwipeToPlayUI;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject SwipeToDrawUI;
    [SerializeField] private GameObject DrawingUI;
    [SerializeField] private GameObject LevelEndUI;

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
        SwipeToPlayUI.SetActive(state == GameState.SwipeToPlay);
        InGameUI.SetActive(state == GameState.InGame);
        SwipeToDrawUI.SetActive(state == GameState.SwipeToDraw);
        DrawingUI.SetActive(state == GameState.Drawing);
        LevelEndUI.SetActive(state == GameState.LevelEnd);
    }
}
