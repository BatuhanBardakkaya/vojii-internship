using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

public class EnemyDestroyer : AgentModuleBase
{
    public int health = 100;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fireball"))
        {
            TakeDamage(25); 
        }else if (collision.gameObject.CompareTag("SpecialFireBall"))
        {
            Debug.Log("Hitted by special");
            TakeDamage(50); 
        }
        
        FireballDestroy(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fireball"))
        {
            Debug.Log("Ontrigger works");
            TakeDamage(25);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); 
        }
    }
    void DestroyFireballDistance(GameObject fireball, int index)
    {
        Destroy(fireball); 
        FireBallController.fireballs.RemoveAt(index); 
        FireBallController.startPositions.Remove(fireball); 
        PlayerFireBallMove.StartForWardVectors.RemoveAt(index); 
    }
    public void FireballDestroy(Collision collision)
    {
        GameObject fireball = collision.gameObject;
        int index = FireBallController.fireballs.IndexOf(fireball);
        if (index != -1) // if hitted object in firaballs list
        {
            DestroyFireballDistance(fireball, index);
        }
    }
}
