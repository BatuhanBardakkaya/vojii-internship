using System.Collections;
using UnityEngine;

namespace Agent.AgentModule
{
    public class CoroutineExample : MonoBehaviour
    {
        public int iterationCount;
        
        private void Start()
        {
            StartCoroutine(IE_CountDown());
        }
        
        private IEnumerator IE_CountDown()
        {
            for (int i = 0; i < iterationCount; i++)
            {
               // Debug.Log("Current Iteration Count : " + i);
                yield return new WaitForSeconds(1f);
            }
        }

    }
}