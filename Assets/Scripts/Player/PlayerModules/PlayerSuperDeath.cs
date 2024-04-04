using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    public class PlayerSuperDeath : PlayerSkillBaseAbstract
    {
        protected override void SkillUsed()
        {
            throw new System.NotImplementedException();
        }
        
        private void OnEnable()
        {
            CoreGameSignals.OnSuperDUsed += StartCooldown;
        }

        private void OnDisable()
        {
            CoreGameSignals.OnSuperDUsed -= StartCooldown;
        }
    }
}