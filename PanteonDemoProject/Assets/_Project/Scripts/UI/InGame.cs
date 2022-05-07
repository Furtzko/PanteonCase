using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject blackoutImg;
    private Animator animator;

    private void Awake()
    {
        EventManager.OnHitObstacle += HitObstacle;
        EventManager.OnLevelRestart += LevelRestart;
    }

    private void OnDestroy()
    {
        EventManager.OnHitObstacle -= HitObstacle;
        EventManager.OnLevelRestart -= LevelRestart;
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = blackoutImg.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HitObstacle()
    {
        animator.SetTrigger("FadeOut");
    }

    private void LevelRestart()
    {
        animator.SetTrigger("FadeIn");
    }
}
