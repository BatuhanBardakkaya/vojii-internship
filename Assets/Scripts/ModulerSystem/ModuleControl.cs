using Assets.Scripts.Agent.AgentModule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ModulerSystem
{
    public class ModuleControl : MonoBehaviour, IModularControl
    {
        [SerializeField] protected IModularSystem[] allChildComponents;

        [SerializeField] public List<AgentModuleBase> AddModules = new List<AgentModuleBase>();
        public List<IModularSystem> Modules { get; set; }
        public Dictionary<Type, IModularSystem> ModuleTypesDictionary { get; set; }
        
        public IEnumerator IE_ActivateModules()
        {
            foreach (var module in Modules)
            {
                if (module != null) yield return module.IE_Activate();
            }
        }

        public virtual void DeactivateModules()
        {
            foreach (var module in Modules)
            {
                if (module != null) module.Deactivate();
            }
        }

        protected virtual void Start()
        {
            Modules = GetComponents<IModularSystem>().ToList();
            allChildComponents = GetComponentsInChildren<IModularSystem>();

            foreach (var component in allChildComponents)
            {
                var modularComponent = component as IModularSystem;
                if (modularComponent != null && !Modules.Any(m => m.GetType() == modularComponent.GetType()))
                {
                    Modules.Add(modularComponent);
                }
            }
            Modules = Modules.OrderBy(m => m.priority).ToList();

            ModuleTypesDictionary = new Dictionary<Type, IModularSystem>();

            foreach (var module in Modules)
            {
                Type t = module.GetType();
                ModuleTypesDictionary.Add(t, module);
            }

            foreach (var module in AddModules)
            {
                Modules.Add(module);
            }

            foreach (var module in Modules)
            {
                StartCoroutine(module.IE_SetParent(this));
                StartCoroutine(module.IE_Initialize());
                StartCoroutine(module.IE_PostInitialize());
                StartCoroutine(module.IE_SetState(ModuleState.Activated));
            }
        }

        
        protected virtual void Update()
        {
            foreach (var module in Modules)
            {
                module.Tick();
            }
        }

        protected virtual void FixedUpdate()
        {
            foreach (var module in Modules)
            {
                module.FixedTick();
            }
        }

        protected virtual void LateUpdate()
        {
            foreach (var module in Modules)
            {
                module.LateTick();
            }
        }

        public T GetModule<T>() where T : IModularSystem
        {
            return (T)ModuleTypesDictionary[typeof(T)];
        }
    }
}