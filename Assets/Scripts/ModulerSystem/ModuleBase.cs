using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ModulerSystem
{

    public abstract class ModuleBase<T> : MonoBehaviour, IModularSystem where T : IModularControl
    {

        [SerializeField] protected T Parent;

        [SerializeField] public int priority { get; set; }
        public ModuleState GeneralState { get; set; }
        public Action<ModuleState> OnStateChangeBegin { get; set; }
        public Action<ModuleState> OnStateChangeEnd { get; set; }

        public  IEnumerator IE_SetState(ModuleState state)
        {
            OnStateChangeBegin?.Invoke(GeneralState);
            GeneralState = state;

            switch (GeneralState)
            {
                case ModuleState.Uninitialized:
                    StartCoroutine(IE_Deinitialize());
                    
                    break;
                case ModuleState.Initialized:
                    StartCoroutine(IE_Initialize());
                    break;
                case ModuleState.PostInitialized:
                    StartCoroutine(IE_PostInitialize());
                    break;
                case ModuleState.Activated:
                    StartCoroutine(IE_Activate());
                    break;
                case ModuleState.Deactivated:
                    Deactivate();
                    break;
                case ModuleState.Disabled:
                    StartCoroutine(IE_Disable());
                    break;
                case ModuleState.Paused:
                    StartCoroutine(IE_Pause());
                    break;
                case ModuleState.Resumed:
                    StartCoroutine(IE_Resume());
                    break;
                case ModuleState.Restarted:
                    StartCoroutine(IE_Restart());
                    break;
                default:
                    
                    throw new ArgumentOutOfRangeException();
            }

            OnStateChangeEnd?.Invoke(GeneralState);
            yield return null;
        }

        public IEnumerator IE_SetParent(IModularControl Parent)
        {
            Parent = (T)Parent;
            yield return null;
            
        }

        public virtual IModularControl GetParent()
        {
            return Parent;
        }

       

        public virtual IEnumerator IE_Deinitialize()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Initialize()
        {
            yield return null;
        }

        public virtual IEnumerator IE_PostInitialize()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Activate()
        {
            gameObject.SetActive(true);
            yield return null;
        }

        

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
           
        }

        public virtual IEnumerator IE_Disable()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Pause()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Resume()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Restart()
        {
            yield return null;
        }
        public abstract void Tick();
        public abstract void FixedTick();
        public abstract void LateTick();

      
    }
}