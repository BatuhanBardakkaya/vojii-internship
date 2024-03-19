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
      private float lerpSpeed = .75f;
      
      
      private void OnEnable()
      {
          CoreGameSignals.OnFireballHit += TakeDamage;
          CoreGameSignals.OnSpecialHit += TakeDamage;
      }
      private void OnDisable()
      {
          CoreGameSignals.OnFireballHit -= TakeDamage;
          CoreGameSignals.OnSpecialHit -= TakeDamage;
      }
      private void Start()
      {
          health = GameManager.Instance.rogEnemystatSo.EnemyStats.Health;
          healthSlider.maxValue = health;
          healthSlider.value = health;
          EasehealthSlider.maxValue = health;
          EasehealthSlider.value = health;
          
      }
      public void TakeDamage(GameObject enemy ,int damage)
      {
          GameObject rootParentGameObject = transform.root.gameObject;
          
          if (enemy == rootParentGameObject)
          {
              
              health -= damage;
              Debug.Log("Damage: " + damage + ", Health left: " + health);

              healthSlider.value = health;

              if (EasehealthSlider.value > health)
              {
                  AnimateSliderValue(EasehealthSlider, health, lerpSpeed);
              }
              
              if (health <= 0)
              {
                  Destroy(rootParentGameObject); 
                  ///////
                  
              }
          }
      }
      public void AnimateSliderValue(Slider easeHealthSlider, float targetHealth, float duration)
      {
          EasehealthSlider.DOValue(targetHealth, duration).SetEase(Ease.Linear);
          
      }
  
}
