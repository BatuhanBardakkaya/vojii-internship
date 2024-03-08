using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Fireball;
using UnityEngine;
using DG.Tweening;

public class PlayerSpawnFireBall : AgentModuleBase
{
   // public GameObject firePoint;
    public List<GameObject> frball = new List<GameObject>();
    public GameObject fireBallController;
    //public GameObject specialFireballPrefab; 
    
    
    public float fireRate = 0.5f; // Ateş etme hızı (saniye cinsinden)
    
    public GameObject Player;
    private Animator anim;
    public GameObject FireBallPrefab;
    public FireBallPool fireBallPool;
    
    public PlayerSpawnSpecialFire specialFireSpawner;

    
    
    
    // private GameObject effectToSpawn;
    public override IEnumerator IE_Initialize()
    {
        //effectToSpawn = frball[0];
        yield return null;
    }
    
    void SpawnFireball()
    {
          /*  GameObject fireballPrefabToUse;
            
            fireballPrefabToUse = frball[0]; 
            GameObject fireball = Instantiate(fireballPrefabToUse, Player.transform.localPosition + Player.transform.forward+new Vector3(0,1,0), Quaternion.identity);
            Vector3 StartForWardVector = Player.transform.forward;
            FireBallController.fireballs.Add(fireball);
            FireBallController.startPositions[fireball] = fireball.transform.position;
            PlayerFireBallMove.StartForWardVectors.Add(StartForWardVector);*/
            
          GameObject fireball = fireBallPool.GetPooledObject(0); // 0, normal ateş topunu temsil ediyor
          if (fireball != null)
          {
              fireball.transform.position = Player.transform.position + Player.transform.forward + new Vector3(0, 1, 0);
              fireball.transform.rotation = Quaternion.identity;
              fireball.SetActive(true);
              Vector3 targetPosition = fireball.transform.position + Player.transform.forward * 40; 
              fireball.transform.DOMove(targetPosition, 2.5f)
                  .SetEase(Ease.Linear).OnComplete(() =>
                          fireball.SetActive(false) // Kullanım bittikten sonra ateş topunu havuza geri koy
                  );
          }
    }

    void SpawnBlueFireBall()
    {
        GameObject fireball = fireBallPool.GetPooledObject(1); // 1, mavi ateş topunu temsil ediyor
        if (fireball != null)
        {
            Vector3 spawnPosition = Player.transform.position + Player.transform.forward + new Vector3(0, 1, 0);
            fireball.transform.position = spawnPosition;
            fireball.transform.rotation = Quaternion.identity;
            fireball.SetActive(true);
            Vector3 targetPosition = spawnPosition + Player.transform.forward * 40;
            fireball.transform.DOMove(targetPosition, 2.5f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutBack).OnComplete(() =>
                        fireball.SetActive(false) // Kullanım bittikten sonra ateş topunu havuza geri koy
                );
        }
    }
}
