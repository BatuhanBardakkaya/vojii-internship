using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlatformTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        // Yalnızca "Player" tagına sahip bir obje geldiğinde tetikleme yap.
        if (other.CompareTag("Player"))
        {
            
            DropPlatformManager.Instance.TriggerEvent();
        }
    }
}
