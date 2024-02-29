using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

public class PlayerSpawnFireBall : AgentModuleBase
{
    public GameObject firePoint;
    public List<GameObject> frball = new List<GameObject>();
    public GameObject fireBallController;
    
    public float fireRate = 0.5f; // Ateş etme hızı (saniye cinsinden)
    private float nextFireTime = 0f;
    public GameObject Player; 

    
    
    
    private GameObject effectToSpawn;
    public override IEnumerator IE_Initialize()
    {
        effectToSpawn = frball[0];
        
        yield return null;
    }

    public override void Tick()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Sonraki ateş için zamanı ayarla
            SpawnFireball();
        }
    }

    void SpawnFireball()
    {
        GameObject fireball;
        fireball = null;
        
        
        if (firePoint != null && effectToSpawn.tag == "Fireball")
        {     
            
            fireball = Instantiate(effectToSpawn, fireBallController.transform);
            fireball.transform.SetPositionAndRotation(firePoint.transform.position,Quaternion.identity);
            Vector3 StartForWardVector = Player.transform.forward;
            FireBallController.fireballs.Add(fireball);
            FireBallController.startPositions[fireball] = fireball.transform.position;
            PlayerFireBallMove.StartForWardVectors.Add(StartForWardVector);
            
        }
        else
        {
                Debug.Log("No Fire Point");
        }
    }
}
