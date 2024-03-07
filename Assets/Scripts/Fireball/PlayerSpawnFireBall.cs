using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Fireball;
using UnityEngine;

public class PlayerSpawnFireBall : AgentModuleBase
{
   // public GameObject firePoint;
    public List<GameObject> frball = new List<GameObject>();
    public GameObject fireBallController;
    //public GameObject specialFireballPrefab; 
    private int shotCount = 0;
    
    public float fireRate = 0.5f; // Ateş etme hızı (saniye cinsinden)
    private float nextFireTime = 0f;
    public GameObject Player;
    private Animator anim;
    public GameObject FireBallPrefab;
    
    public PlayerSpawnSpecialFire specialFireSpawner;

    
    
    
    // private GameObject effectToSpawn;
    public override IEnumerator IE_Initialize()
    {
        //effectToSpawn = frball[0];
        
        
        yield return null;
    }

   

    
    void SpawnFireball()
    {
        GameObject fireballPrefabToUse;
        
       
        fireballPrefabToUse = frball[0]; // Diğer atışlarda varsayılan fireball
        
        
       
            GameObject fireball = Instantiate(fireballPrefabToUse, Player.transform.localPosition + Player.transform.forward+new Vector3(0,1,0), Quaternion.identity);
            
            Vector3 StartForWardVector = Player.transform.forward;
            FireBallController.fireballs.Add(fireball);
            FireBallController.startPositions[fireball] = fireball.transform.position;
            PlayerFireBallMove.StartForWardVectors.Add(StartForWardVector);
      
    }
}
