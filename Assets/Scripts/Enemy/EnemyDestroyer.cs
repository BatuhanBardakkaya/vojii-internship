using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.Player.PlayerModules;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDestroyer : AgentModuleBase
{
    public int health;
    private Animator _animator;

    private void Start()
    {
        health = GameManager.Instance.rogEnemystatSo.EnemyStats.Health;
        _animator = GetComponent<Animator>();
    }

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
    
    public void TakeDamage(GameObject enemy,int damage)
    {   
        if (enemy == this.gameObject)
        { 
            _animator.SetTrigger("GetHit");   
            
        }
    }
}
