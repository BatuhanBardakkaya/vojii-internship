using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public GameObject fadePanel; // FadePanel UI Image referansı
    public float fadeDuration = 1.0f; // Fade in/out süresi

    private Image fadeImage;

    private void Start()
    {
        fadeImage = fadePanel.GetComponent<Image>();
        fadePanel.SetActive(false); // Başlangıçta panel gizli
    }

    // Sahne yüklemesi için kullanılacak fonksiyon
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    // Sahneye geçmeden önce fade out
    IEnumerator FadeOut(string sceneName)
    {
        fadePanel.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0); // Tamamen şeffaf

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}