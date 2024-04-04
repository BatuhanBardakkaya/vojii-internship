using System;
using Assets.Scripts.Player.PlayerModules;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Firaball.Boss
{
    public class BossBars : MonoBehaviour
    {
        public Slider healthSlider;
        public Slider EasehealthSlider;
        public float health;
        public float Maxhealth;
        public float experience;
        private float lerpSpeed = .75f;
        public EnemyLootbag<Transform> lootTable;
        
        private enum BossStage { Stage1, Stage2, Stage3 }
        private BossStage currentStage = BossStage.Stage1;
        
        private ParticleSystem BossParticleSystem;
        
        private void OnEnable()
        {
            CoreGameSignals.OnFireballHit += TakeDamage;
            CoreGameSignals.OnSpecialHit += TakeDamage;
            CoreGameSignals.OnSuperDeathHit += TakeDamage;
        }
        private void OnDisable()
        {
            CoreGameSignals.OnFireballHit -= TakeDamage;
            CoreGameSignals.OnSpecialHit -= TakeDamage;
            CoreGameSignals.OnSuperDeathHit -= TakeDamage;
        }

        private void Start()
        {
            health = GameManager.Instance.bosEnemyStatsSo.EnemyStats.Health;
            Maxhealth = health;
            experience = GameManager.Instance.bosEnemyStatsSo.EnemyStats.Experiance;
            healthSlider.maxValue = health;
            healthSlider.value = health;
            EasehealthSlider.maxValue = health;
            EasehealthSlider.value = health;
            BossParticleSystem = GameObject.FindGameObjectWithTag("BossTrail").GetComponent<ParticleSystem>();
        }

        public void TakeDamage(GameObject enemy ,int damage)
        {
            GameObject rootParentGameObject = transform.parent.parent.gameObject;
          
            if (enemy == rootParentGameObject)
            {
                Debug.Log("Hitted:"+damage);
                health -= damage;
                healthSlider.value = health;

                if (EasehealthSlider.value > health)
                {
                    Debug.Log("Can:" + health);
                    AnimateSliderValue(EasehealthSlider, health, lerpSpeed);
                }

                CheckHealthForStageChange();
              
                if (health <= 0)
                {
                    SpawnLoot();
                    //rootParentGameObject.SetActive(false);
                    CoreGameSignals.OnGetExperiance?.Invoke(experience);
                    EnemyGameSignals.OnEnemyKilled?.Invoke(enemy);
                  
                }
            }
        }
        public void AnimateSliderValue(Slider easeHealthSlider, float targetHealth, float duration)
        {
            EasehealthSlider.DOValue(targetHealth, duration).SetEase(Ease.Linear);
          
        }
        void SpawnLoot()
        {
            if (lootTable != null)
            {
                Transform lootItem = lootTable.GetRandom(); 
                Instantiate(lootItem, transform.position, Quaternion.identity); 
            }
        }
        
        private void CheckHealthForStageChange()
        {
            float healthPercentage = health / Maxhealth;

            if (healthPercentage <= 0.5f && currentStage == BossStage.Stage1)
            {
                ChangeStage(BossStage.Stage2);
            }
            else if (healthPercentage <= 0.25f && currentStage == BossStage.Stage2)
            {
                ChangeStage(BossStage.Stage3);
            }
        }

        private void ChangeStage(BossStage newStage)
        {
            if (currentStage != newStage) // Sahne zaten bu sahnede değilse
            {
                currentStage = newStage;
                switch (newStage)
                {
                    case BossStage.Stage2:
                        BossParticleSystem.Play();
                        Debug.Log("Boss Stage 2 activated.");
                        EnemyGameSignals.OnBossStage2?.Invoke();
                        break;
                    case BossStage.Stage3:
                        Debug.Log("Boss Stage 3 activated.");
                        EnemyGameSignals.OnBossStage3?.Invoke();
                        break;
                }
            }
        }
        
    }
}