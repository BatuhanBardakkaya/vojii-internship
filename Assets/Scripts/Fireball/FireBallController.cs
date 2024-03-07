using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Agent.AgentModule;
using UnityEngine;

public class FireBallController : AgentControlBase
{
    public static List<GameObject> fireballs = new List<GameObject>();
    public static List<GameObject> specialFireballs = new List<GameObject>();
    public static Dictionary<GameObject, Vector3> startPositions = new Dictionary<GameObject, Vector3>();
    
    public static IEnumerator DestroySpecialFireballAfterDelay(GameObject fireball, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (fireball != null)
        {
            specialFireballs.Remove(fireball);
            Destroy(fireball);
        }
    }
    
}
