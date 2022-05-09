using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager._onStateChanged(GameState.Drawing);
    }
}
