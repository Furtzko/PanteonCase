using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera inGameCamera;
    [SerializeField] private CinemachineVirtualCamera drawingCamera;
    [SerializeField] private CinemachineVirtualCamera finalCamera;

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
        switch (state)
        {
            case GameState.SwipeToStart:
                inGameCamera.Priority = 1;
                drawingCamera.Priority = 0;
                finalCamera.Priority = 0;
                break;
            case GameState.Drawing:
                inGameCamera.Priority = 0;
                drawingCamera.Priority = 1;
                finalCamera.Priority = 0;
                break;
            case GameState.LevelEnd:
                inGameCamera.Priority = 0;
                drawingCamera.Priority = 0;
                finalCamera.Priority = 1;
                break;
        }

    }

}
