using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public GameObject fadePanel; // FadePanel UI Image referansı
    public float fadeDuration = 1.0f; // Fade out süresi

    private Image fadeImage;

    private void Start()
    {
        fadeImage = fadePanel.GetComponent<Image>();
        StartCoroutine(FFadeOut());
    }

    // Sahneye girişte fade out
    protected virtual IEnumerator FFadeOut()
    {
        fadePanel.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 1); // Tamamen siyah başlat

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1 - t / fadeDuration); // Yavaş yavaş şeffaf hale getirin
            yield return null;
        }

        fadePanel.SetActive(false); // Tamamen şeffaf olduğunda paneli kapat
    }
}