using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.MoverThings;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ModulerSystem
{
    public enum ModuleState
    {
        Uninitialized,
        Initialized,
        PostInitialized,
        Activated,
        Deactivated,
        Disabled,
        Paused,
        Resumed,
        Restarted
    }
    public interface IModularSystem
    {
        public ModuleState GeneralState { get; set; }
        public int priority {get; }
        public Action<ModuleState> OnStateChangeBegin
        {
            get; set;
        }
        public Action<ModuleState> OnStateChangeEnd
        {
            get; set;
        }
        public IEnumerator IE_SetState(ModuleState state);
        public IEnumerator IE_SetParent(IModularControl Parent);
        public IModularControl GetParent();
        
        //***********************************************//
        public IEnumerator IE_Deinitialize();
        public IEnumerator IE_Initialize();

        public IEnumerator IE_PostInitialize();
        public IEnumerator IE_Activate();
        public  void Deactivate();
        public IEnumerator IE_Disable();
        public IEnumerator IE_Pause();
        public IEnumerator IE_Resume();
        public IEnumerator IE_Restart();

       
        public void Tick();

        public void FixedTick();

        public void LateTick();
       
        
    }
}