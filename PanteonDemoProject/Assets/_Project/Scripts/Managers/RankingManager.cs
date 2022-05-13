using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : BaseSingleton<RankingManager>
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform[] Opponents;
    private List<float> racerZPositions;
    private float playerCurrentPos;

    private int playerRank;
    private int playerCurrentRank = 0;
    public int PlayerRank => playerRank;

    void Update()
    {
        RankCalc();
    }

    //Tüm karakterlerin z pozisyonu listeye alýnýp sýralanýr.
    private void RankCalc()
    {
        racerZPositions = new List<float>();
        for(int i= 0; i< Opponents.Length; i++)
        {
            racerZPositions.Add(Opponents[i].position.z);
        }
        racerZPositions.Add(Player.position.z);
        playerCurrentPos = Player.position.z;

        racerZPositions.Sort();

        playerRank = 11 - racerZPositions.IndexOf(playerCurrentPos);

        if(playerRank != playerCurrentRank)
        {
            EventManager._onRankChanged();
            playerCurrentRank = playerRank;
        }

    }
}
