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
        EventManager.OnScreenFadeOut += ScreenFadeOut;
        EventManager.OnScreenFadeIn += ScreenFadeIn;
        EventManager.OnRankChanged += RankChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnScreenFadeOut -= ScreenFadeOut;
        EventManager.OnScreenFadeIn -= ScreenFadeIn;
        EventManager.OnRankChanged -= RankChanged;
    }


    void Start()
    {
        blackoutAnimator = blackoutImg.GetComponent<Animator>();
        rankAnimator = rank.GetComponent<Animator>();
    }

    private void Update()
    {
        rank.GetComponent<TextMeshProUGUI>().text = "#" + RankingManager.Instance.PlayerRank;
    }


    private void ScreenFadeOut()
    {
        blackoutAnimator.SetTrigger("FadeOut");
    }

    private void ScreenFadeIn()
    {
        blackoutAnimator.SetTrigger("FadeIn");
    }

    private void RankChanged()
    {
        rankAnimator.SetTrigger("posChanged");
    }
}
