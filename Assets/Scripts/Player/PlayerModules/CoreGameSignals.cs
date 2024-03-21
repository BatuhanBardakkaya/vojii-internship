using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModules
{
    public static class CoreGameSignals
    {

        public static Action<GameObject, int> OnFireballHit;

        public static Action<GameObject, int> OnSpecialHit;
        
        public static Action<GameObject, int> OnPlayerTakeDamage;

        public static Action HitTaked;
        
        public static Action OnDashUsed;

        public static Action OnGlideUsed;
        
        public static Action<Transform> OnEnemySpawn;
        
        
        
        
        
        
    }
    
}