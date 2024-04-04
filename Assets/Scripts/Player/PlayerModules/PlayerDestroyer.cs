using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
    public int health;
    private Animator _animator;

    private void Start()
    {
    
        _animator = GetComponent<Animator>();
        health = GameManager.Instance.playerstatsSo.PlayerStats.Health;
    }

    public void OnEnable()
        {
           
            CoreGameSignals.OnPlayerTakeDamage += TakeDamage;
            CoreGameSignals.OnPlayerDeath += PlayerDestroy;
        }
    
        public void OnDisable()
        {
            CoreGameSignals.OnPlayerTakeDamage -= TakeDamage;
            CoreGameSignals.OnPlayerDeath -= PlayerDestroy;

        }
        
        public void TakeDamage(GameObject enemy,int damage)
        {
            _animator.SetTrigger("GetHit");
        }

        private void PlayerDestroy()
        {
            Destroy(this.gameObject);
        }
}
