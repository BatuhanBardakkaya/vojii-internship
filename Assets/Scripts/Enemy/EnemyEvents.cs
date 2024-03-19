using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

public enum Attacks
{
    RogueAttack,
    WarriorAttack
}
public class EnemyEvents : MonoBehaviour
{
    public Attacks attacks;
    private int damage;
    
    private void Start()
    {
        switch (attacks)
        {
            case Attacks.RogueAttack:
                damage = GameManager.Instance.rogEnemystatSo.EnemyStats.Damage;
            break;
            case Attacks.WarriorAttack:
                damage = GameManager.Instance.warEnemystatSo.EnemyStats.Damage;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoreGameSignals.OnPlayerTakeDamage?.Invoke(other.gameObject,damage);
            CoreGameSignals.HitTaked?.Invoke();
            this.gameObject.SetActive(false);
            //Debug.Log("Player Take Damage" + damage);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoreGameSignals.OnPlayerTakeDamage?.Invoke(other.gameObject,damage);
            CoreGameSignals.HitTaked?.Invoke();
        }
    }
}


