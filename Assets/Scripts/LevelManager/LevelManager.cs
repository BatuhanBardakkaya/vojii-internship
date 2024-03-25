using Assets.Scripts.Agent.AgentModule;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : AgentModuleBase
{
    private List<IResettable> resettables;

    void Awake()
    {
        resettables = new List<IResettable>(FindObjectsOfType<MonoBehaviour>().OfType<IResettable>());
    }

    public override void Tick()
    {
        base.Tick();
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetLevel();
            
        }
        

        
        
    }

    
    public void ResetLevel()
    {
        foreach (var resettable in resettables)
        {
            resettable.ResetToInitialState();
        }
    }
}
