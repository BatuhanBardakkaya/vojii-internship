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
    private bool isShieldActive = false;
    
    private void OnEnable()
    {
        CoreGameSignals.OnPlayerTakeDamage += TakeDamage;
        CoreGameSignals.OnHealthPotionUsed += IncreaseHealth;
        PlayerSkill.OnShieldStateChange += UpdateShieldStatus;
    }

    private void OnDisable()
    {
        CoreGameSignals.OnPlayerTakeDamage -= TakeDamage;
        CoreGameSignals.OnHealthPotionUsed -= IncreaseHealth;
        PlayerSkill.OnShieldStateChange -= UpdateShieldStatus;
    }

    private void Start()
    {
        health = GameManager.Instance.playerstatsSo.PlayerStats.Health;
        maxHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = health;
        EasehealthSlider.maxValue = health;
        EasehealthSlider.value = health;
        
    }
    
    public void TakeDamage(GameObject enemy ,int damage)
    {
            if (!isShieldActive)
            {
                health -= damage;
            
                healthSlider.value = health;

                if (EasehealthSlider.value > health)
                {
                    AnimateSliderValue(EasehealthSlider, health, lerpSpeed);
                }

                if (health <=0)
                {
                    CoreGameSignals.OnPlayerDeath?.Invoke();
                }
            }
        
    }
    public void AnimateSliderValue(Slider easeHealthSlider, float targetHealth, float duration)
    {
        EasehealthSlider.DOValue(targetHealth, duration).SetEase(Ease.Linear);
          
    }

    private void UpdateShieldStatus(bool isActive)
    {
        isShieldActive = isActive;
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
            healthSlider.value = health;
            Debug.Log("Can2"+health);
        }   
        
    }
}
