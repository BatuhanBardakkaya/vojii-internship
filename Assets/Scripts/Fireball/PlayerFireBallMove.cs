using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFireBallMove : AgentModuleBase
{
    [SerializeField] public float fireBallSpeed;
   
    public static List<Vector3> StartForWardVectors = new List<Vector3>();
    
   
  
    
  public override void Tick()
  {
      
      for (int i = FireBallController.fireballs.Count - 1; i >= 0; i--)
      { 
          GameObject fireball = FireBallController.fireballs[i];
          if (fireBallSpeed != 0)
          {
             // FireBallController.fireballs[i].transform.position += StartForWardVectors[i] * (fireBallSpeed * Time.deltaTime);
             fireball.transform.position += StartForWardVectors[i] * (fireBallSpeed * Time.deltaTime);
            
             // Mesafe kontrolü
             float distance = Vector3.Distance(FireBallController.startPositions[fireball],fireball.transform.position);
             if (distance > 80) 
             {
                 DestroyFireball(fireball, i);
             }
             
          }
          else
          {
              Debug.Log("No Speed");
          }
          
      }
  }

  
  void DestroyFireball(GameObject fireball, int index)
  {
      Destroy(fireball); 
      FireBallController.fireballs.RemoveAt(index); 
      FireBallController.startPositions.Remove(fireball); 
      PlayerFireBallMove.StartForWardVectors.RemoveAt(index); 
  }
}
