using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DropPlatformAction : MonoBehaviour,IResettable
{
    public GameObject[] platforms;
    public TextMeshProUGUI runText;
    private Vector3[] _initialPositions;
    private bool[] _kinematicStates;
    public AudioSource RUN;
    void Awake()
    {
        RUN = GetComponent<AudioSource>();
        // Platformların başlangıç pozisyonlarını ve kinematik durumlarını kaydet
        _initialPositions = new Vector3[platforms.Length];
        _kinematicStates = new bool[platforms.Length];
        for (int i = 0; i < platforms.Length; i++)
        {
            _initialPositions[i] = platforms[i].transform.position;
            _kinematicStates[i] = platforms[i].GetComponent<Rigidbody>().isKinematic;
        }
    }
    
    public IEnumerator PerformAction()
    {   
        runText.gameObject.SetActive(true);
        RUN.Play();
        // Geri sayım için başlangıç değeri
        int countdown = 3;

        // Geri sayım sıfıra ulaşana kadar döngü
        while (countdown > 0)
        {
            // Metni güncelle
            runText.text = "YOU BETTER RUN!! " + countdown;
            // Her saniyede bir azalt
            yield return new WaitForSeconds(1);
            countdown--;
        }
        runText.gameObject.SetActive(false);
        
        foreach (GameObject platform in platforms)
        {
            Debug.Log("Actoin is Working");
            Rigidbody rb = platform.GetComponent<Rigidbody>();
            rb.isKinematic = false; 
            yield return new WaitForSeconds(3);
        }
    
    }

    public void ResetToInitialState()
    {
        // Bu kısımı tamamlayın
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].transform.position = _initialPositions[i];
            Rigidbody rb = platforms[i].GetComponent<Rigidbody>();
            rb.isKinematic = _kinematicStates[i];
            if(!_kinematicStates[i]) // Eğer kinematik değilse, hareketi sıfırla
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        // Eğer metin gösterimi tekrar başlatılmak istenmiyorsa, bu kısmı yorum satırı yapın.
        runText.gameObject.SetActive(false);
    }
}
