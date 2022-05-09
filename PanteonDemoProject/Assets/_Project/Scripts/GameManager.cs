using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    public GameState currentState;

    private void Start()
    {
        UpdateGameState(GameState.SwipeToPlay);
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;
        EventManager._onStateChanged(newState);
    }

}

public enum GameState
{
    SwipeToPlay,
    InGame,
    SwipeToDraw,
    Drawing,
    LevelEnd
}