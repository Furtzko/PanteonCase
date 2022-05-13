using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawing : MonoBehaviour
{
    [SerializeField] private GameObject text;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (text.activeInHierarchy && Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Wall"))
                {
                    text.SetActive(false);
                    StartCoroutine(CompleteLevel());
                }
            }
        }
    }

    private IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(5f);
        EventManager._onStateChanged(GameState.LevelEnd);
    }

}
