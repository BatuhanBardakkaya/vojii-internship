using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.Player.PlayerModules;
using DG.Tweening;
using Inventory;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PlayerBars : AgentModuleBase
{
    public Slider healthSlider;
    public Slider EasehealthSlider;
    public float maxHealth;
    public float health;
    private float lerpSpeed = 0.75f;

    
    private void OnEnable()
    {
        CoreGameSignals.OnPlayerTakeDamage += TakeDamage;
        CoreGameSignals.OnHealthPotionUsed += IncreaseHealth;
    }

    private void OnDisable()
    {
        CoreGameSignals.OnPlayerTakeDamage -= TakeDamage;
        CoreGameSignals.OnHealthPotionUsed -= IncreaseHealth;
    }

    private void Start()
    {
        health = GameManager.Instance.playerstatsSo.PlayerStats.Health;
        maxHealth = health;
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

    public void IncreaseHealth(int value)
    {
        if (health>=maxHealth)
        {
            Debug.Log("Your Health is full");
            health = maxHealth;
            Debug.Log("Can2"+health);
        }
        else
        {
            health += value;
            Debug.Log("Can2"+health);
        }    
        
        
        
    }
}
