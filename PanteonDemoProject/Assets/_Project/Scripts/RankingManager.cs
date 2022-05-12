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
    public int PlayerRank => playerRank;

    void Update()
    {
        RankCalc();
    }

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

        /*
        Debug.Log("**********************");
        Debug.Log("----------------------");
        foreach (float a in racerZPositions)
        {
            Debug.Log(a);
        }
        Debug.Log("----------------------");
        Debug.Log(playerRank);
        Debug.Log("**********************");
        */
    }
}
