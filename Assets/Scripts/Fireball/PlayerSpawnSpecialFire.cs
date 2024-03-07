using System.Collections;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

namespace Fireball
{
    public class PlayerSpawnSpecialFire : AgentModuleBase
    {
        public GameObject specialFireballPrefab;
        public Transform playerTransform;
        public override IEnumerator IE_Initialize()
        {
            yield return null;
        }

        public void SpawnSpecialFireball()
        {
            GameObject specialFireball = Instantiate(specialFireballPrefab, transform.root.localPosition + transform.root.forward*2, Quaternion.identity);
            specialFireball.transform.rotation = Quaternion.LookRotation(playerTransform.forward);
            Debug.Log("SpawnSpecial");
            StartCoroutine(DestroyAfterDelay(specialFireball, 3));
            
        }
      
        private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(obj);
        }
    }
}