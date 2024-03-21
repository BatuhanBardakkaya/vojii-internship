using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyGameSignals
{
   public static Action<bool> OnEnemyAreaEntered;
   public static Action<int> OnEnemiesSpawned;
   public static Action OnEnemyKilled;
   

}
