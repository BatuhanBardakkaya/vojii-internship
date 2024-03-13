using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.Player.PlayerModules;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDestroyer : AgentModuleBase
{
    public int health = 100;
   

    public void OnEnable()
    {
        CoreGameSignals.OnFireballHit += TakeDamage;
        CoreGameSignals.OnSpecialHit += TakeDamage;
    }

    public void OnDisable()
    {
        CoreGameSignals.OnFireballHit -= TakeDamage;
        CoreGameSignals.OnSpecialHit -= TakeDamage;
    }
    
    public void TakeDamage(GameObject enemy,int damage)// burası olmayacak aslında
    {
        if (enemy == this.gameObject)
        { 
         health -= damage;
            if (health <= 0)
            {
                Destroy(this.gameObject); 
            }
        }
    }
}
