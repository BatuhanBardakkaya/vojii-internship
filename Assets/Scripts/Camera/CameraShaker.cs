using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    private CinemachineImpulseSource impulse;
    
    private void Start()
    {
        impulse = transform.GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        CoreGameSignals.HitTaked += ShakeCamera;
    }

    private void OnDisable()
    {
        CoreGameSignals.HitTaked -= ShakeCamera;
    }
    
    
    public void ShakeCamera()
    {
        Debug.Log("ShakecameraWorks");
        impulse.GenerateImpulse(1f);
        
    }

    
    
    
    
}
