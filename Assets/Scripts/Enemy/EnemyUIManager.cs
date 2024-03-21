using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    public TextMeshProUGUI enemyCounterText; 
    private int totalEnemiesSpawned = 0;
    private int enemiesKilled = 0;

    private void OnEnable()
    {
        EnemyGameSignals.OnEnemyAreaEntered += ToggleEnemyCounterText;
        EnemyGameSignals.OnEnemiesSpawned += UpdateTotalEnemiesSpawned;
        EnemyGameSignals.OnEnemyKilled += UpdateEnemiesKilled;
        
    }

    private void OnDisable()
    {
        EnemyGameSignals.OnEnemyAreaEntered -= ToggleEnemyCounterText;
        EnemyGameSignals.OnEnemiesSpawned -= UpdateTotalEnemiesSpawned;
        EnemyGameSignals.OnEnemyKilled -= UpdateEnemiesKilled;
    }

    private void ToggleEnemyCounterText(bool show)
    {
        enemyCounterText.gameObject.SetActive(show);
    }
    
    private void UpdateTotalEnemiesSpawned(int spawnedEnemies)
    {
        totalEnemiesSpawned += spawnedEnemies;
        UpdateEnemyCounterText();
    }

    private void UpdateEnemiesKilled()
    {
        enemiesKilled++;
        UpdateEnemyCounterText();
    }
    
    private void UpdateEnemyCounterText()
    {
        enemyCounterText.text = $"{enemiesKilled} / {totalEnemiesSpawned}";
    }
}
