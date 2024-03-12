using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    public PlayerStatsSO playerstatsSo;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
                Destroy(gameObject);
        }

        playerstatsSo = GetPlayerStat();
    }

    private PlayerStatsSO GetPlayerStat()
    {
        return Resources.Load<PlayerStatsSO>("PlayerStats");
    }
}
