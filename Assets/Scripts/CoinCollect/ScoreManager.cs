using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 
public class ScoreManager : MonoBehaviour , IResettable
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    private int Coin = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        Coin += scoreToAdd;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Coin: " + Coin;
    }

    public void ResetToInitialState()
    {
        Coin = 0; 
        UpdateScoreUI();
    }
}
