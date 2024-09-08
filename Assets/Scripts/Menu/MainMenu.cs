using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Fade fadeScript; // FadeScript referansı
    public GameObject mainMenuPanel;  // Ana menü paneli
    public GameObject howToPlayPanel; // How to Play paneli

    // Play butonu için fonksiyon
    public void PlayGame()
    {
        
        Time.timeScale = 1;
        Debug.Log("Playe basıldı");
        fadeScript.FadeToScene("MyScene2"); // Oyun sahnesine geçiş
    }

    // Quit butonu için fonksiyon
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    // How to Play butonuna tıklandığında çağrılacak fonksiyon
    public void ShowHowToPlay()
    {
        mainMenuPanel.SetActive(false);    // Ana menü panelini gizle
        howToPlayPanel.SetActive(true);    // How to Play panelini göster
    }

    // Çarpı butonuna tıklandığında çağrılacak fonksiyon
    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);   // How to Play panelini gizle
        mainMenuPanel.SetActive(true);     // Ana menü panelini tekrar göster
    }
}