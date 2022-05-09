using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EndLevel());
        }
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.5f);
        EventManager._onStateChanged(GameState.SwipeToDraw);
    }
}
