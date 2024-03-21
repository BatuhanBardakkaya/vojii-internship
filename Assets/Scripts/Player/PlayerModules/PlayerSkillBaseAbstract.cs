using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerSkillBaseAbstract : MonoBehaviour
{
    public Image skillImage;
    public TextMeshProUGUI skillText;
    public float cooldownTime;
    
    protected Tweener fillTween;

    protected abstract void SkillUsed();

    public void StartCooldown()
    {
        skillImage.fillAmount = 1f;
        skillText.gameObject.SetActive(true);

        fillTween = DOTween.To(() => skillImage.fillAmount, x => skillImage.fillAmount = x, 0f, cooldownTime)
            .SetEase(Ease.Linear)
            .OnUpdate(UpdateCooldownText)
            .OnComplete(CooldownComplete);
    }

    private void UpdateCooldownText()
    {
        float remainingTime = cooldownTime - fillTween.Elapsed();
        skillText.text = remainingTime.ToString("F0");
    }

    private void CooldownComplete()
    {
        skillText.gameObject.SetActive(false);
        SkillUsed(); 
    }
}
