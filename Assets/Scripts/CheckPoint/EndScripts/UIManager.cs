using System.Collections;
using UnityEngine;

namespace Assets.Scripts.CheckPoint
{
    public class UIManager : MonoBehaviour
    {
        public GameObject textPanel;

        private void OnEnable()
        {
            EndTrigger.OnEndTriggerEnter += ShowTextPanel;
            EndTrigger.OnEndTriggerExit += HideTextPanel;
        }

        private void OnDisable()
        {
            EndTrigger.OnEndTriggerEnter -= ShowTextPanel;
            EndTrigger.OnEndTriggerExit -= HideTextPanel;
        }

        void ShowTextPanel()
        {
            textPanel.SetActive(true);
            Debug.Log("Text Panel Activated");
        }

        void HideTextPanel()
        {
            textPanel.SetActive(false);
            Debug.Log("Text Panel Deactivated");
        }

    }
}