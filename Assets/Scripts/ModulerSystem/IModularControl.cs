using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ModulerSystem
{
    public interface IModularControl
    {
        public List<IModularSystem> Modules { get; set; }
        public Dictionary<Type, IModularSystem> ModuleTypesDictionary { get; set; }
        
       // public IEnumerator IE_ActivateModules();
        //public IEnumerator IE_DeactivateModules();
    }
}