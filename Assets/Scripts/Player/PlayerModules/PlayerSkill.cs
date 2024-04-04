using System;
using System.Collections;
using System.Diagnostics;
using Assets.Scripts.Agent.AgentModule;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


namespace Assets.Scripts.Player.PlayerModules
{
    public class PlayerSkill : AgentModuleBase
    {
        public static event Action<bool> OnShieldStateChange;
        
        private ParticleSystem SuperDParticleSystem;
        private ParticleSystem ShieldParticleSystem;

        private float SuperDCooldownTimeLeft;
        private float ShieldCooldownTimeLeft;

        [SerializeField] private float SuperDCooldown = 7f;
        [SerializeField] private float ShieldCooldown = 10f;

        private bool isSuperD;
        private bool isShield;

        [SerializeField] private float ShieldDuration = 2f;
        private float ShieldTimeLeft;
        
        [SerializeField] private float SuperDDuration = .25f;
        private float SuperDTimeLeft;

        
        public override IEnumerator IE_Initialize()
        {
            SuperDParticleSystem = GameObject.FindGameObjectWithTag("SuperD").GetComponent<ParticleSystem>();
            ShieldParticleSystem = GameObject.FindGameObjectWithTag("Shield").GetComponent<ParticleSystem>();

            SuperDParticleSystem.gameObject.SetActive(false);
            ShieldParticleSystem.gameObject.SetActive(false);
            SuperDCooldownTimeLeft = 0;
            ShieldCooldownTimeLeft = 0;


            yield return null;
        }

        public override void Tick()
        {
            UpdateShield();
            UpdateSuperD();
            if (SuperDCooldownTimeLeft > 0)
            {
                SuperDCooldownTimeLeft -= Time.deltaTime;
            }

            if (ShieldCooldownTimeLeft > 0)
            {
                ShieldCooldownTimeLeft -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) // Dash input
            {
                
                SuperDeath();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Shield();
            }

            base.Tick();
        }

        public void SuperDeath()
        {
            if (SuperDCooldownTimeLeft <= 0)
            {
                CoreGameSignals.OnSuperDUsed?.Invoke();
                isSuperD = true;
                //Animate Here
                SuperDParticleActive();
                SuperDTimeLeft = SuperDDuration;
                SuperDCooldownTimeLeft = SuperDCooldown;
                
            }
        }

        public void Shield()
        {
            if (ShieldCooldownTimeLeft<=0)
            {
                CoreGameSignals.OnShieldUsed?.Invoke();
                isShield = true;
                //Animate Here
                ShieldParticleActive();
                ShieldTimeLeft = ShieldDuration;
                ShieldCooldownTimeLeft = ShieldCooldown;
                OnShieldStateChange?.Invoke(isShield);
            }
            
        }
        
        private void SuperDParticleActive()
            {
                if (SuperDParticleSystem != null)
                {
                    if (isSuperD == true)
                    {
                        SuperDParticleSystem.gameObject.SetActive(true);
                        SuperDParticleSystem.Play();
                        
                    }
                    else
                    {  
                        SuperDParticleSystem.gameObject.SetActive(false);
                        SuperDParticleSystem.Stop();
                    }
                }
            }

        private void ShieldParticleActive()
        {
            if (ShieldParticleSystem!=null)
            {
                if (isShield == true)
                {
                    ShieldParticleSystem.gameObject.SetActive(true);
                    ShieldParticleSystem.Play();
                }
                else
                {
                    ShieldParticleSystem.gameObject.SetActive(false);
                    ShieldParticleSystem.Stop();
                }
            }
        }

        private void UpdateSuperD()
        {
            if (isSuperD)
            {
                SuperDTimeLeft -= Time.deltaTime;
                if (SuperDTimeLeft <= 0)
                {
                    isSuperD = false;
                    SuperDParticleActive();
                }
                
                
            }
        }
        private void UpdateShield()
        {
            if (isShield)
            {
                ShieldTimeLeft -= Time.deltaTime;
                if (ShieldTimeLeft <= 0)
                {
                    isShield = false;
                    ShieldParticleActive();
                    OnShieldStateChange?.Invoke(isShield);
                }
                
                
            }
            
        }
        
        
    }
}
