using System.Collections;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

namespace Fireball
{
    public class PlayerSpawnSpecialFire : AgentModuleBase
    {
        public FireBallPool fireBallPool; // Havuz yöneticisi referansı
        public int specialFireballPoolId = 0;
       // public GameObject specialFireballPrefab;
        public Transform playerTransform;
        public override IEnumerator IE_Initialize()
        {
            yield return null;
        }

        public void SpawnSpecialFireball()
        {
            // Havuzdan bir özel ateş topu nesnesi çek
            GameObject specialFireball = fireBallPool.GetPooledObject(specialFireballPoolId);
            if (specialFireball != null)
            {
                specialFireball.transform.position = transform.root.localPosition + transform.root.forward * 2;
                specialFireball.transform.rotation = Quaternion.LookRotation(playerTransform.forward);
                
                // Nesneyi aktifleştir
                specialFireball.SetActive(true);
                
                Debug.Log("SpawnSpecial");
                
                // Kullanımdan sonra nesneyi havuza geri koymak için gecikmeli yok etme işlemini başlat
                StartCoroutine(DeactivateAfterDelay(specialFireball, 3));
            }
        }
      
        private IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            // Havuza geri koymak için nesneyi devre dışı bırak
            obj.SetActive(false);
        }
    }
}
