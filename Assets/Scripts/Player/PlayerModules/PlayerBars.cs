using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.Player.PlayerModules;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PlayerBars : AgentModuleBase
{
    public Slider healthSlider;
    public Slider EasehealthSlider;
    public float maxHealth = 100f;
    public float health;
    private float lerpSpeed = 0.01f;
    public override IEnumerator IE_Initialize()
    {
        
        return base.IE_Initialize();
    }

    public PlayerStats playerstats;
  /*  private void OnEnable()
    {
        CoreGameSignals.OnEnemyTakeDamage += TakeDamage;
    }

    private void OnDisable()
    {
        CoreGameSignals.OnEnemyTakeDamage -= TakeDamage;
    }
*/
    private void Start()
    {
        health = maxHealth;
        
    }

    private void Update() //Enemy için ayrı yap
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            //TakeDamage(20);
            
        }
        if (healthSlider.value != EasehealthSlider.value)
        {
            EasehealthSlider.value = Mathf.Lerp(EasehealthSlider.value, health, lerpSpeed);
        }
        
    }

    /*public override void Tick()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            takedamage(20);
            Debug.Log("j ye bastık"+health);
        }
        
        base.Tick();
    }*/

   
    public void TakeDamage(int damage)
    {
        
       // damage = playerstats.Damage;
        health -= damage;
    }
    
}
