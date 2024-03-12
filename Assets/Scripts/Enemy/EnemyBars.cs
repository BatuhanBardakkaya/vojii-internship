using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class EnemyBars : MonoBehaviour
{
      public Transform cam;
      public Slider healthSlider;
      public Slider EasehealthSlider;
      public float maxHealth = 100f;
      public float health;
      private float lerpSpeed = .75f;
     
  
    
      private void OnEnable()
      {
          CoreGameSignals.OnEnemyTakeDamage += TakeDamage;
      }
  
      private void OnDisable()
      {
          CoreGameSignals.OnEnemyTakeDamage -= TakeDamage;
      }
  
      private void Start()
      {
          health = maxHealth;
          healthSlider.maxValue = maxHealth;
          healthSlider.value = maxHealth;
          EasehealthSlider.maxValue = maxHealth;
          EasehealthSlider.value = maxHealth;
          
      }
  
      private void Update() //Enemy için ayrı yap
      {
             if (healthSlider.value != health)
          {
              healthSlider.value = health;
          }

          if (Input.GetKeyDown(KeyCode.B))
          {
              int damage = GameManager.Instance.playerstatsSo.PlayerStats.FireBallDamage;
              Debug.Log("Damage: "+damage);
          }
          
         /* if (EasehealthSlider.value > health)
          {
              AnimateSliderValue(EasehealthSlider, health, lerpSpeed);
          }*/
          
      }
  
     
      public void TakeDamage(int damage)
      {
          damage = GameManager.Instance.playerstatsSo.PlayerStats.FireBallDamage;
          health -= damage;
          Debug.Log("Damage: "+damage);
          // health değerini doğrudan kullanarak EasehealthSlider'ın değerini animasyonla güncelle
          if (EasehealthSlider.value > health)
          {
              AnimateSliderValue(EasehealthSlider, health, lerpSpeed);
          }
      }

      // Slider'ınızın değerini hedef sağlık değerine yumuşak bir şekilde animasyonlu olarak değiştirme
      public void AnimateSliderValue(Slider easeHealthSlider, float targetHealth, float duration)
      {
          EasehealthSlider.DOValue(targetHealth, duration).SetEase(Ease.Linear);
          //easeHealthSlider.DOKill(complete: true);
      }
  
}
