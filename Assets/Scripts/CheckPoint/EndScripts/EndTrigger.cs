using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts.ModulerSystem;
using Assets.Scripts.Agent.AgentModule;

public class EndTrigger : AgentModuleBase
{

    // Events
    public static event Action OnEndTriggerEnter;
    public static event Action OnEndTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OntriggerEnter Here");
            OnEndTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OntriggerExit Here");
            OnEndTriggerExit?.Invoke();
        }
    }



}
