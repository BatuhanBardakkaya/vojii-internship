using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollect : MonoBehaviour, IResettable
{
    public int CoinValue = 1;
    private Vector3 startPosition;
    public AudioSource collectSound;

    void Awake()
    {
        startPosition = transform.position;
        collectSound = GameObject.Find("CollectSound").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(CoinValue);
            gameObject.SetActive(false);
            collectSound.Play();
        }
    }

    public void ResetToInitialState()
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
    }
}
