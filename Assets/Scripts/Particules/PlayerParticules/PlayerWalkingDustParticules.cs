using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingDustParticules : MonoBehaviour
{
    public ParticleSystem LeftFootParticleSystem; // Inspector'dan atayın
    public ParticleSystem RightFootParticleSystem; // Inspector'dan atayın
    public bool leftfoot = true;

    // Emit metodunu çağıracak fonksiyon
    public void EmitParticles(int amount)
    {
        if (leftfoot == true)
        {
            leftfoot = false;
            LeftFootParticleSystem.Emit(amount);
            
        }
        else
        {
            leftfoot = true;
            RightFootParticleSystem.Emit(amount);
            
        }
         
    }
}
