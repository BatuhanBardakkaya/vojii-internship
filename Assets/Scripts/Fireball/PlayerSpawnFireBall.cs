using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Fireball;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlayerSpawnFireBall : AgentModuleBase
{
    
    public GameObject Player;
    private Animator anim;
    public FireBallPool fireBallPool;
    
    public override IEnumerator IE_Initialize()
    {

        yield return null;
    }
    
    void SpawnFireball()
    {
        
          GameObject fireball = fireBallPool.GetPooledObject(0); // 0, normal ateş topunu temsil ediyor
          if (fireball != null)
          {
              fireball.transform.position = Player.transform.position + Player.transform.forward + new Vector3(0, 1, 0);
              fireball.transform.rotation = Quaternion.identity;
              fireball.SetActive(true);
              Vector3 targetPosition = fireball.transform.position + Player.transform.forward * 40; 
              fireball.transform.DOMove(targetPosition, 2.5f)
                  .SetEase(Ease.Linear).OnComplete(() =>
                          fireball.SetActive(false) 
                  );
          }
    }

    void SpawnBlueFireBall()
    {
        GameObject fireball = fireBallPool.GetPooledObject(1); 
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
