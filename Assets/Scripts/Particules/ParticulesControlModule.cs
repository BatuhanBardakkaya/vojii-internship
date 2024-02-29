using System.Collections;
using UnityEngine;
using System;
using Assets.Scripts.Agent.AgentModule;

namespace Assets.Scripts.Particules
{
    public class ParticulesControlModule : AgentModuleBase,IResettable
    {
        private ParticleSystem[] _particleSystems;
        private bool[] _activated; 
        private Vector3[] _initialPositions; 
        private ParticleSystem.MainModule[] _initialMainModules;

        public override IEnumerator IE_Initialize()
        {
            StartCoroutine(base.IE_Initialize());
            
            _particleSystems = FindObjectsOfType<ParticleSystem>();
            _activated = new bool[_particleSystems.Length];
            _initialPositions = new Vector3[_particleSystems.Length];
            _initialMainModules = new ParticleSystem.MainModule[_particleSystems.Length];

            StarLocationGet();
           
            StartAndStopAllParticleSystems();
            Debug.Log("particleSystems dizisi başlatılıyor, boyut: " + (_particleSystems != null ? _particleSystems.Length.ToString() : "null"));
            yield return null;

        }
        public override void Tick()
        {
            base.Tick();
           
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartAndStopAllParticleSystems();
            }
          
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeAllParticleSystemsColorToRandom();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(ActivateParticleSystemsInRandomOrderWithDelay());
            }
           
            if (Input.GetKeyDown(KeyCode.K))
            {
                StopAllParticleSystems();
            }

        }
        public void ResetToInitialState()
        {
            if (_particleSystems == null) return;

            for (int i = 0; i < _particleSystems.Length; i++)
            {
                if (_particleSystems[i] == null) continue;
               
                _particleSystems[i].transform.position = _initialPositions[i];
                _particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _particleSystems[i].Clear();

                
                var mainModule = _particleSystems[i].main;
                mainModule.startColor = _initialMainModules[i].startColor;

                _particleSystems[i].Play();
            }
            ResetActivationStates(); 
        }

        private void StarLocationGet()
        {
           
            for (int i = 0; i < _particleSystems.Length; i++)
            {
                _initialPositions[i] = _particleSystems[i].transform.position;
                _initialMainModules[i] = _particleSystems[i].main;
            }
        }
        private void StartAndStopAllParticleSystems()
        {
            foreach (ParticleSystem ps in _particleSystems)
            {
                ps.Play();
                StartCoroutine(StopParticleSystemAfterDelay(ps, 4f));
            }
        }

        IEnumerator StopParticleSystemAfterDelay(ParticleSystem ps, float delay)
        {
            yield return new WaitForSeconds(delay);
            ps.Stop(); 
        }
      
        IEnumerator StartParticleSystemAfterDelay(ParticleSystem ps, float delay)
        {
            yield return new WaitForSeconds(delay);
            ps.Play();
        }
        void StartEachParticleSystemAtRandomTime()
        {
            foreach (ParticleSystem ps in _particleSystems)
            {
                float delay = UnityEngine.Random.Range(1f, 10f); // 1 - 10 seconds random delay
                StartCoroutine(StartParticleSystemAfterDelay(ps, delay));
            }
        }
        void ChangeAllParticleSystemsColorToRandom()
        {
            foreach (ParticleSystem ps in _particleSystems)
            {
                var mainModule = ps.main;
                mainModule.startColor = new ParticleSystem.MinMaxGradient(GetRandomColor());
            }
        }
        IEnumerator ActivateParticleSystemsInRandomOrderWithDelay()
        {
            ResetActivationStates();

            int systemsToActivate = _particleSystems.Length;
            while (systemsToActivate > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, _particleSystems.Length);
                if (!_activated[randomIndex])
                {
                    _particleSystems[randomIndex].Play();
                    _activated[randomIndex] = true;
                    systemsToActivate--;

                   
                    yield return new WaitForSeconds(5);
                }
            }
        }

        void ResetActivationStates()
        {
            if (_particleSystems == null) {
                Debug.Log("Our Particle System is Empty");
            }

            for (int i = 0; i < _activated.Length; i++)
            {
                _activated[i] = false;
            }
        }
        Color GetRandomColor()
        {
           
            return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }
        void StopAllParticleSystems()
        {
            foreach (ParticleSystem ps in _particleSystems)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }

    }
}