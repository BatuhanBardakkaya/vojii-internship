using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using Firaball;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyStatsSo enemystatSo;
    
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
        enemystatSo = GetEnemyStat();
    }
    private EnemyStatsSo GetEnemyStat()
    {
        return Resources.Load<EnemyStatsSo>("EnemyStats");
    }
    private PlayerStatsSO GetPlayerStat()
    {
        return Resources.Load<PlayerStatsSO>("PlayerStats");
    }
    
}
