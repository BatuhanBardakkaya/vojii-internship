using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    public static class CoreGameSignals
    {
        public static Action<int> OnEnemyTakeDamage;

        public static Action<GameObject, int> OnFireballHit;

        public static Action<GameObject, int> OnSpecialHit;
        
        public static Action<GameObject, int> OnPlayerTakeDamage;

        public static Action HitTaked;
        
        public static Action<Transform> OnEnemySpawn;
        
        
        
        
        
    }
    
}