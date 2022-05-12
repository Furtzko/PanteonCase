using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeToStart : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            EventManager._onStateChanged(GameState.InGame);
        }

    }
}
