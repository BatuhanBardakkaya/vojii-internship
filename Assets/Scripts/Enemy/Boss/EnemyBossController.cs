using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : MonoBehaviour
{
    public int maxHealth = 1000;
    private int currentHealth;
    private enum BossStage { Stage1, Stage2, Stage3 }
    private BossStage currentStage = BossStage.Stage1;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CheckHealthForStageChange()
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        // Canın yarısının altına düştüğünde Stage 2'ye geçiş
        if (healthPercentage <= 0.5f && currentStage == BossStage.Stage1)
        {
            ChangeStage(BossStage.Stage2);
        }
        // Canın çeyreğinin altına düştüğünde Stage 3'e geçiş
        else if (healthPercentage <= 0.25f && currentStage == BossStage.Stage2)
        {
            ChangeStage(BossStage.Stage3);
        }
    }

    private void ChangeStage(BossStage newStage)
    {
        currentStage = newStage;
        // Burada her aşama için özel davranışlar tetiklenebilir.
        switch (newStage)
        {
            case BossStage.Stage2:
                // Stage 2 için özel davranışlar
                Debug.Log("Boss Stage 2 activated.");
                break;
            case BossStage.Stage3:
                // Stage 3 için özel davranışlar
                Debug.Log("Boss Stage 3 activated.");
                break;
        }
    }
}
