using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallPool : MonoBehaviour
{
    [SerializeField] private Transform FirePoint;
    [System.Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;

    }

    [SerializeField] private Pool[] pools = null;
    
    private void Awake()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].pooledObjects = new Queue<GameObject>();
            for (int j = 0; j < pools[i].poolSize; j++)
            {
                GameObject obj = Instantiate(pools[i].objectPrefab,FirePoint);
                obj.SetActive(false);
                
                pools[i].pooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }
        
        GameObject obj = pools[objectType].pooledObjects.Dequeue();
        //obj.SetActive(true);
        pools[objectType].pooledObjects.Enqueue(obj);
        return obj;
        
    }
    
}
