using System.Collections;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player.PlayerModules
{
    public class PlayerAttack : AgentModuleBase
    {

        public Animator animator;
        public int attackCount = 0;
        public bool Attack;
        
        public override IEnumerator IE_Initialize()
        {
            animator = GetComponent<Animator>();
            yield return null;
        }
        public override void Tick()
        {
            Combo();
           
            if (Input.GetKeyDown(KeyCode.H) && !IsPointerOverUI())
            {
                animator.SetTrigger("BlueFireBall");
            }
        }
        public void Combo()
        {
            if (Input.GetMouseButtonDown(0)&& !Attack && !IsPointerOverUI())
            {
                Attack = true;
                animator.SetTrigger(""+attackCount);
            }
        }
        public void Start_Combo()
        {
            Attack = false;
            if (attackCount<3)
            {
                attackCount++;
            }
        }
        public void Finish_Ani()
        {
            Attack = false;
            attackCount = 0;
        }
        
        
        bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
        
    }
    
}
      
