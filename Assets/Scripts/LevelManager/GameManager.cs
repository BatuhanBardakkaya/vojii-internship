using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using Firaball;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public EnemyStatsSo rogEnemystatSo;
    
    public EnemyStatsSo warEnemystatSo;
    
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
        rogEnemystatSo = GetRogEnemyStat();
        warEnemystatSo = GetWarEnemyStatsSo();
        
        
    }
   
    private EnemyStatsSo GetRogEnemyStat()
    {
        return Resources.Load<EnemyStatsSo>("RogueEnemyStats");
    }

    private EnemyStatsSo GetWarEnemyStatsSo()
    {
        return Resources.Load<EnemyStatsSo>("WarriorEnemyStats");
    }
    private PlayerStatsSO GetPlayerStat()
    {
        return Resources.Load<PlayerStatsSO>("PlayerStats");
    }
    
}
