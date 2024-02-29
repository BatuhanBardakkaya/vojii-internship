using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

public class FireBallController : AgentControlBase
{
    public static List<GameObject> fireballs = new List<GameObject>();
    public static Dictionary<GameObject, Vector3> startPositions = new Dictionary<GameObject, Vector3>();
    
    
}
