using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
   [SerializeField] private GameObject _uiPanel;
   [SerializeField] private TextMeshProUGUI _promptText;
   public bool isDisplayed = false;
   

   private void Start()
   {
      _uiPanel.SetActive(false);
   }

   public void SetPromptUp(string promptText)
   {
      _promptText.text = promptText;
      _uiPanel.SetActive(true);
      isDisplayed = true;

   }

   public void Close()
   {
      _uiPanel.SetActive(false);
      isDisplayed = false;
   }
   
}
