using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    public class PlayerShield : PlayerSkillBaseAbstract
    {
        protected override void SkillUsed()
        {
            throw new System.NotImplementedException();
        }
        
        private void OnEnable()
        {
            CoreGameSignals.OnShieldUsed += StartCooldown;
        }

        private void OnDisable()
        {
            CoreGameSignals.OnShieldUsed -= StartCooldown;
        }
    }
}