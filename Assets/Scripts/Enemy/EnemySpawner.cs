using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

public class EnemySpawner : AgentModuleBase
{
    public GameObject cubePrefab; 
    public int numberOfCubes = 5; 
    public float _minX;
    public float _maxX;
    public float _maxY;
    public float _minY;
    public float _minZ;
    public float _maxZ;
    public AudioSource Fight;
    private bool trigered = false;
    

    private void OnTriggerEnter(Collider other)
    {
        //Fight.GetComponent<AudioSource>();
        if (trigered != true)
        {
        if (other.CompareTag("Player"))
        {
            trigered = true;
            Fight.Play();
            for (int i = 0; i < numberOfCubes; i++)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(_minX, _maxX),
                    Random.Range(_minY, _maxY),
                    Random.Range(-_minZ, -_maxZ) 
                ) + transform.position; 

                Instantiate(cubePrefab, spawnPosition, Quaternion.identity); 
            }
        }
        }
    }
}
