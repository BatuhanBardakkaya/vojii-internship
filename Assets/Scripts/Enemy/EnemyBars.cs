using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class EnemyBars : MonoBehaviour
{
      public Slider healthSlider;
      public Slider EasehealthSlider;
      public float health;
      public float experience;
      private float lerpSpeed = .75f;
      public Enemies Enemies;
      public EnemyLootbag<Transform> lootTable;
      
      
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
          switch (Enemies)
          {
              case Enemies.Rogue:
                  health = GameManager.Instance.rogEnemystatSo.EnemyStats.Health;
                  experience = GameManager.Instance.rogEnemystatSo.EnemyStats.Experiance;
                  healthSlider.maxValue = health;
                  healthSlider.value = health;
                  EasehealthSlider.maxValue = health;
                  EasehealthSlider.value = health;
                  Debug.Log("Rog Enemy"+ health);
                  Debug.Log("Rog experience"+ experience);
                  break;
              case Enemies.Warrior:
                  health = GameManager.Instance.warEnemystatSo.EnemyStats.Health;
                  experience = GameManager.Instance.warEnemystatSo.EnemyStats.Experiance;
                  healthSlider.maxValue = health;
                  healthSlider.value = health;
                  EasehealthSlider.maxValue = health;
                  EasehealthSlider.value = health;
                  Debug.Log("War Enemy:"+ health);
                  Debug.Log("War experience:"+ experience);
                  break;
          }
          
          
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
                  AnimateSliderValue(EasehealthSlider, health, lerpSpeed);
              }
              
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
              Instantiate(lootItem, transform.position, Quaternion.identity); // Düşmanın konumunda öğeyi yarat
          }
      }
  
}
