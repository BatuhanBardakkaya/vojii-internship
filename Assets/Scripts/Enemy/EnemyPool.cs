using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    [System.Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledEnemies;
        public GameObject enemyPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools = null;

    private void Awake()
    {
        InitializePools();
    }

    private void InitializePools()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].pooledEnemies = new Queue<GameObject>();
            for (int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject enemy = Instantiate(pools[i].enemyPrefab, spawnPoint);
                enemy.SetActive(false);
                pools[i].pooledEnemies.Enqueue(enemy);
            }
        }
    }

    public GameObject GetPooledEnemy(int enemyType)
    {
        if (enemyType >= pools.Length)
        {
            Debug.LogError("Requested enemy type is out of range.");
            return null;
        }

        GameObject enemy = pools[enemyType].pooledEnemies.Dequeue();
        pools[enemyType].pooledEnemies.Enqueue(enemy);
        return enemy;
    }
}
