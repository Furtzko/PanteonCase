using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    public GameState currentState;

    private void Awake()
    {
        EventManager.OnStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnStateChanged -= GameStateChanged;
    }

    private void Start()
    {
        EventManager._onStateChanged(GameState.SwipeToStart);
    }


    private void GameStateChanged(GameState state)
    {
        currentState = state;

    }

    public void Exit()
    {
        Application.Quit();
    }

}

public enum GameState
{
    SwipeToStart,
    InGame,
    Drawing,
    LevelEnd,
    LevelFail
}