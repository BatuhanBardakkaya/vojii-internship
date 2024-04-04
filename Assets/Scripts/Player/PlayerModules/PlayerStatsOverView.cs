using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    public class PlayerStatsOverView : MonoBehaviour
    {
        public TMP_Text HealthText;
        public TMP_Text FireballText;
        public TMP_Text SpecialText;
        public TMP_Text SuperDeathText;

        public float Health;
        public float Fireball;
        public float Special;
        public float SuperDeath;

        public GameObject characterPanel;
        private void Start()
        {
            Health = GameManager.Instance.playerstatsSo.PlayerStats.Health;
            Fireball = GameManager.Instance.playerstatsSo.PlayerStats.FireBallDamage;
            Special = GameManager.Instance.playerstatsSo.PlayerStats.SpecialDamage;
            SuperDeath = GameManager.Instance.playerstatsSo.PlayerStats.SuperDeathDamage;

            HealthText.text = "Health: " + Health;
            FireballText.text = "Fireball: " + Fireball;
            SpecialText.text = "Special: " + Special;
            SuperDeathText.text = "Super Death:" + SuperDeath;

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                characterPanel.SetActive(!characterPanel.activeSelf); // Panelin aktiflik durumunu değiştir
            }
        }
    }
}