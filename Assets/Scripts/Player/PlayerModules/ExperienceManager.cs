using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.PlayerModules;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceManager : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;

    float currentLevel, totalExperience;
    float previousLevelsExperience, nextLevelsExperience;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;
    
    //--------//

    private ParticleSystem LevelupParticleSystem;
    void Start()
    {
        currentLevel = 1;
        LevelupParticleSystem = GameObject.FindGameObjectWithTag("Levelup").GetComponent<ParticleSystem>();
        UpdateLevel();
    }

    private void OnEnable()
    {
        CoreGameSignals.OnGetExperiance += AddExperience;
    }

    private void OnDisable()
    {
        CoreGameSignals.OnGetExperiance -= AddExperience;
    }

    public void AddExperience(float amount)
    {
        totalExperience += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    void CheckForLevelUp()
    {
        if(totalExperience >= nextLevelsExperience)
        {
            currentLevel++;
            UpdateLevel();

            LevelupParticleSystem.Play();
        }
    }

    void UpdateLevel()
    {
        previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel-1);
        nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
        UpdateInterface();
    }

    void UpdateInterface()
    {
        float start = totalExperience - previousLevelsExperience;
        float end = nextLevelsExperience - previousLevelsExperience;

        levelText.text = currentLevel.ToString();
        experienceText.text = start + " exp / " + end + " exp";
        
        float currentFill = experienceFill.fillAmount;
        float targetFill = (float)start / (float)end;
        experienceFill.DOFillAmount(targetFill, 0.5f).SetEase(Ease.OutQuad); 
    }
}
