using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatformManager : MonoBehaviour
{
    public static DropPlatformManager Instance { get; private set; }
    public DropPlatformAction action;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerEvent()
    {
        // Tetikleyici bir olay tetiklendiğinde, eylemi başlat
        Debug.Log("Trigger Enter Work");
        StartCoroutine(action.PerformAction());
    }
}
