using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    private NavMeshAgent agent;
    
    [SerializeField] private Transform playerTransform;
     // Oyuncunun Transformunu saklamak için

    void Awake()
    {
        
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    
        // Player'ı bul (Örneğin, tüm player objeleri "Player" tag'ine sahipse)
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void OnEnable()
    {
        // Oyuncu spawn olduğunda çalışacak metod
        CoreGameSignals.OnEnemySpawn += OnEnemySpawned;
    }

    void OnDisable()
    {
        CoreGameSignals.OnEnemySpawn -= OnEnemySpawned;
    }

    private void OnEnemySpawned(Transform playerTransform)
    {
        // Oyuncunun Transformunu sakla ve navigasyon hedefini güncelle
        this.playerTransform = playerTransform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Oyuncunun anlık konumuna doğru hareket et
            agent.SetDestination(playerTransform.position);
            this.transform.LookAt(playerTransform);
        }
    }
}
