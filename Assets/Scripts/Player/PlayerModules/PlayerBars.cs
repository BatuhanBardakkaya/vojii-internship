using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.Player.PlayerModules;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PlayerBars : AgentModuleBase
{
    public Slider healthSlider;
    public Slider EasehealthSlider;
    public float maxHealth = 100f;
    public float health;
    private float lerpSpeed = 0.75f;

    
    private void OnEnable()
    {
        CoreGameSignals.OnPlayerTakeDamage += TakeDamage;
    }

    private void OnDisable()
    {
        CoreGameSignals.OnPlayerTakeDamage -= TakeDamage;
    }

    private void Start()
    {
        health = GameManager.Instance.playerstatsSo.PlayerStats.Health;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        EasehealthSlider.maxValue = health;
        EasehealthSlider.value = health;
        Debug.Log("Player Bars Health:"+health);
        
    }
    
    public void TakeDamage(GameObject enemy ,int damage)
    {
            health -= damage;
           // Debug.Log("Damage: " + damage + ", Health left: " + health);

            healthSlider.value = health;

            if (EasehealthSlider.value > health)
            {
                AnimateSliderValue(EasehealthSlider, health, lerpSpeed);
            }
        
    }
    public void AnimateSliderValue(Slider easeHealthSlider, float targetHealth, float duration)
    {
        EasehealthSlider.DOValue(targetHealth, duration).SetEase(Ease.Linear);
          
    }
    
}
