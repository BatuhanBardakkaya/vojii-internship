using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyFireBall : MonoBehaviour
{
    public FireBallPool fireBallPool;

    private void Start()
    {
        fireBallPool = FindObjectOfType<FireBallPool>();
        if (fireBallPool == null)
        {
            Debug.LogError("FireBallPool objesi sahnede bulunamadı!");
        }
    }

    void EnemySpawnFireball()
    {
        
        GameObject fireball = fireBallPool.GetPooledObject(3); 
        if (fireball != null)
        {
            fireball.transform.position = this.transform.position + this.transform.forward + new Vector3(0, 1, 0);
            fireball.transform.rotation = Quaternion.identity;
            fireball.SetActive(true);
            Vector3 targetPosition = fireball.transform.position + this.transform.forward * 40; 
            fireball.transform.DOMove(targetPosition, 2.5f)
                .SetEase(Ease.Linear).OnComplete(() =>
                        fireball.SetActive(false) // Kullanım bittikten sonra ateş topunu havuza geri koy
                );
        }
    }

}
