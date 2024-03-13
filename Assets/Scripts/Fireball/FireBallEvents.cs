using System;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;

namespace Fireball
{
    public enum Skills
    {
        Fireball,
        SpecialFireball,
        Special
    }
    public class FireBallEvents : MonoBehaviour
    {

        public Skills skills;
        private int damage;
        private void Start()
        {
            switch (skills)
            {
                case Skills.Fireball:
                    damage  = GameManager.Instance.playerstatsSo.PlayerStats.FireBallDamage;
                    break;
                case Skills.SpecialFireball:
                    damage  = GameManager.Instance.playerstatsSo.PlayerStats.BlueFireBallDamage;
                    break;
                case Skills.Special:
                    damage  = GameManager.Instance.playerstatsSo.PlayerStats.SpecialDamage;
                    break;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy_Skeleton"))
            {
                CoreGameSignals.OnFireballHit?.Invoke(other.gameObject,damage);
                this.gameObject.SetActive(false);
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            
            if (other.gameObject.CompareTag("Enemy_Skeleton"))
            {
                
                CoreGameSignals.OnSpecialHit?.Invoke(other.gameObject,damage);
                //this.gameObject.SetActive(false);
            }
        }
    }
}