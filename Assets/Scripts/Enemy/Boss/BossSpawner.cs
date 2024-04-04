using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public int numberOfEachEnemies = 1; 
    public GameObject spawnCenter; // Enemy'lerin etrafında spawn olacağı GameObject
    public float spawnRadius = 1f; // Spawn center etrafındaki maksimum mesafe
    public EnemyPool enemyPool; 
    private bool triggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            //Fight.Play();
            

            for (int i = 0; i < numberOfEachEnemies; i++)
            {
                Vector3 spawnPosition1 = RandomPositionAroundCenter();
                
                GameObject enemy1 = enemyPool.GetPooledEnemy(2); // 0, Enemy1 tipini belirtir
                
                if (enemy1 != null)
                {
                    enemy1.transform.position = spawnPosition1;
                    enemy1.transform.rotation = Quaternion.identity;
                    enemy1.SetActive(true); // Enemy'yi aktifleştir
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
