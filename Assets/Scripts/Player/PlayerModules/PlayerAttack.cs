using System.Collections;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    public class PlayerAttack : AgentModuleBase
    {

        private Animator animator;
        private int attackCount = 0; // Saldırı sayısını takip etmek için bir sayac

        public override IEnumerator IE_Initialize()
        {
            animator = GetComponent<Animator>();
            yield return null;
        }

        public override void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                attackCount++; // Saldırı sayısını artır
                if (attackCount % 3 == 0)
                {
                    
                    animator.SetTrigger("SpecialFireBall");
                }
                else
                {
                    animator.SetTrigger("FireBall");
                }
            }
        }
    }
}
      
