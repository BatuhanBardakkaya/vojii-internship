using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Canvas ya da Pause Menu UI'nı buraya drag&drop yap
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                
            }
            else
            {
                PauseGame();
                
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);  // Canvas'ı gizler
        GameManager.Instance.ToggleCursor(false);
        Time.timeScale = 1f;  // Oyun zamanını devam ettirir
        isPaused = false;
        
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // Canvas'ı görünür yapar
        GameManager.Instance.ToggleCursor(true);
        Time.timeScale = 0f;  // Oyun zamanını durdurur
        isPaused = true;
        
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;  // Zamanı normale döndür (ana menüye geçerken oyun zamanını normale al)
        SceneManager.LoadScene("MainMenu");  // Ana menü sahnesine geçiş yapar
    }
    
    
}