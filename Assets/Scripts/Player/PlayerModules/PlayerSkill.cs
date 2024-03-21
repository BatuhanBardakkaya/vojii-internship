using System;
using System.Diagnostics;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum PlayerSkills
{
    Dash,
    Glide
}

namespace Assets.Scripts.Player.PlayerModules
{
    public class PlayerSkill : MonoBehaviour
    {
        public PlayerSkills playerskills;
        private Tweener filltween;
        public Image Dash;
        public Image Glide;
        public TextMeshProUGUI Dash_Text;
        
        public float timer;

 
        private void OnEnable()
        {
            CoreGameSignals.OnDashUsed += StartTimer;
            //CoreGameSignals.OnGlideUsed += StartTimer;
        }

        private void OnDisable()
        {
            CoreGameSignals.OnDashUsed -= StartTimer;
           // CoreGameSignals.OnGlideUsed -= StartTimer;
        }
        
        private void StartTimer()
        {
            Image FillAmout;
            switch (playerskills)
            {
                case PlayerSkills.Dash:
                    FillAmout = Dash;
                    break;
                case PlayerSkills.Glide:
                    FillAmout = Glide;
                    break;
            }
            Dash.fillAmount = 1f;
            Dash_Text.gameObject.SetActive(true);
            
            filltween = DOTween.To(() => Dash.fillAmount, x => Dash.fillAmount = x, 0f, timer)
                .SetEase(Ease.Linear)
                .OnUpdate(UpdateTimerText)
                .OnComplete(FillComplete);

        }
        private void FillComplete()
        {
            Dash_Text.gameObject.SetActive(false);
        }
        private void UpdateTimerText()
        {
            float RemainTime=timer-filltween.Elapsed();
            Dash_Text.text = RemainTime.ToString("F0");
        }
    }
    
    
}