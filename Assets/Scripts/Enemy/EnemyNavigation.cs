using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;
using UnityEngine.AI;

public enum Enemies
{
    Rogue,
    Warrior
}

public class EnemyNavigation : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator _animator;
    private float time;
    public Enemies Enemies;

    [SerializeField] private Transform playerTransform;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        switch (Enemies)
        {
            case Enemies.Rogue:
                time = GameManager.Instance.rogEnemystatSo.EnemyStats.CooldDown;
                break;
            case Enemies.Warrior:
                time = GameManager.Instance.warEnemystatSo.EnemyStats.CooldDown;
                break;

        }
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
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

                switch (Enemies)
                {
                    case Enemies.Rogue:
                        
                        if (distanceToPlayer <= GameManager.Instance.rogEnemystatSo.EnemyStats.AttackDistance)
                        {
                            agent.SetDestination(transform.position);
                            time += Time.deltaTime;
                            if (time >= GameManager.Instance.rogEnemystatSo.EnemyStats.CooldDown)
                            {

                                agent.radius = Mathf.Lerp(agent.radius, 5f, Time.deltaTime*50);
                                    
                                time = 0;
                                _animator.SetTrigger("RangeAttack");
                            }

                        }
                        else
                        {

                            agent.SetDestination(playerTransform.position);
                            
                        }

                        break;
                    case Enemies.Warrior:

                        if (distanceToPlayer <= GameManager.Instance.warEnemystatSo.EnemyStats.AttackDistance)
                        {
                            agent.SetDestination(transform.position);
                            time += Time.deltaTime;
                            if (time >= GameManager.Instance.warEnemystatSo.EnemyStats.CooldDown)
                            {
                                agent.radius = Mathf.Lerp(agent.radius, 1.5f, Time.deltaTime*25); 
                                time = 0;
                                _animator.SetTrigger("Attack");
                            }
                        }
                        else
                        {
                            agent.SetDestination(playerTransform.position);
                         
                        }

                        break;

                }
                
                float speed = agent.velocity.magnitude;
                _animator.SetFloat("Speed", speed);

                Vector3 lookDirection = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                transform.LookAt(lookDirection);
            }
        }
    }


