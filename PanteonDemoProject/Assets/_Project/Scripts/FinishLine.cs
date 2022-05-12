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
        else if(GameManager.Instance.currentState.Equals(GameState.InGame) && other.CompareTag("Opponent"))
        {
            EventManager._onStateChanged(GameState.LevelFail);
        }
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.5f);
        EventManager._onStateChanged(GameState.Drawing);
    }
}
