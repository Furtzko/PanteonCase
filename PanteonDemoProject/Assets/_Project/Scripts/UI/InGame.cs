using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject blackoutImg;
    [SerializeField] private GameObject rank;
    private Animator blackoutAnimator;
    private Animator rankAnimator;

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


    void Start()
    {
        blackoutAnimator = blackoutImg.GetComponent<Animator>();
        //rankAnimator = rank.GetComponent<Animator>();
    }

    private void Update()
    {
        rank.GetComponent<TextMeshProUGUI>().text = "#" + RankingManager.Instance.PlayerRank;
    }


    private void HitObstacle()
    {
        blackoutAnimator.SetTrigger("FadeOut");
    }

    private void LevelRestart()
    {
        blackoutAnimator.SetTrigger("FadeIn");
    }
}
