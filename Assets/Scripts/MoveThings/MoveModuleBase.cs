using Assets.Scripts.Agent.AgentModule;
using Assets.Scripts.ModulerSystem;
using Assets.Scripts.MoverThings;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MoveThings
{
    public class MoveModuleBase : ModuleBase<MoveControlBase>
    {

        public override IEnumerator IE_Initialize()
        {
            yield return null;
        }

        public override IEnumerator IE_Deinitialize()
        {
            yield return null;
        }

        public override IEnumerator IE_PostInitialize()
        {
            yield return null;
        }

        public override IEnumerator IE_Activate()
        {
            yield return null;
        }

        public override void  Deactivate()
        {
            
        }

        public override IEnumerator IE_Disable()
        {
            yield return null;
        }

        public override IEnumerator IE_Pause()
        {
            yield return null;
        }

        public override IEnumerator IE_Resume()
        {
            yield return null;
        }

        public override IEnumerator IE_Restart()
        {
            yield return null;
        }

        public override void Tick()
        {
        }

        public override void FixedTick()
        {
        }

        public override void LateTick()
        {
        }
    }
}