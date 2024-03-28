using System.Collections;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

namespace Fireball
{
    public class PlayerSpawnSpecialFire : AgentModuleBase
    {
        public FireBallPool fireBallPool; 
        public int specialFireballPoolId = 0;
        public Transform playerTransform;
        public override IEnumerator IE_Initialize()
        {
            yield return null;
        }

        public void SpawnSpecialFireball()
        {

            GameObject specialFireball = fireBallPool.GetPooledObject(specialFireballPoolId);
            if (specialFireball != null)
            {
                specialFireball.transform.position = transform.root.localPosition + transform.root.forward * 2;
                specialFireball.transform.rotation = Quaternion.LookRotation(playerTransform.forward);
                
                specialFireball.SetActive(true);
                
                Debug.Log("SpawnSpecial");
                
                StartCoroutine(DeactivateAfterDelay(specialFireball, 3));
            }
        }
      
        private IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            obj.SetActive(false);
        }
    }
}
