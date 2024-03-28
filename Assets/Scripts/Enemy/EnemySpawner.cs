using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

public class EnemySpawner : AgentModuleBase
{
    public int numberOfEachEnemies = 5; 
    public GameObject spawnCenter; // Enemy'lerin etrafında spawn olacağı GameObject
    public float spawnRadius = 10f; // Spawn center etrafındaki maksimum mesafe
    public EnemyPool enemyPool; // EnemyPool referansı
    private bool triggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            //Fight.Play();
            int totalEnemies = numberOfEachEnemies * 2;

            for (int i = 0; i < numberOfEachEnemies; i++)
            {
                Vector3 spawnPosition1 = RandomPositionAroundCenter();
                Vector3 spawnPosition2 = RandomPositionAroundCenter();

                // Enemy1 ve Enemy2'yi pool'dan çek
                GameObject enemy1 = enemyPool.GetPooledEnemy(0); // 0, Enemy1 tipini belirtir
                GameObject enemy2 = enemyPool.GetPooledEnemy(1); // 1, Enemy2 tipini belirtir
                
                if (enemy1 != null)
                {
                    enemy1.transform.position = spawnPosition1;
                    enemy1.transform.rotation = Quaternion.identity;
                    enemy1.SetActive(true); // Enemy'yi aktifleştir
                }

                if (enemy2 != null)
                {
                    enemy2.transform.position = spawnPosition2;
                    enemy2.transform.rotation = Quaternion.identity;
                    enemy2.SetActive(true); // Enemy'yi aktifleştir
                }
            }
        }
    }

    Vector3 RandomPositionAroundCenter()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += spawnCenter.transform.position;
        randomDirection.y = spawnCenter.transform.position.y; // Yüksekliği sabit tutmak istiyorsanız
        return randomDirection;
    }
}
