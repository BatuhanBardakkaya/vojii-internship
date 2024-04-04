using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyGameSignals
{
   public static Action<bool> OnEnemyAreaEntered;
   public static Action<int> OnEnemiesSpawned;
   public static Action <GameObject> OnEnemyKilled;
   public static Action OnBossStage2;
   public static Action OnBossStage3;


}
