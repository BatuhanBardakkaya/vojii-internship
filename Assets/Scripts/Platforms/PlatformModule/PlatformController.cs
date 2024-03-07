using Assets.Scripts.MoveThings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MoveModuleBase
{
    public Rigidbody Rigidbody;
    public float speed;
    public float forcespeed;
    public float time;
    public float interval;


    public bool timer;

    

    public override IEnumerator IE_Initialize()
    {
        StartCoroutine(base.IE_Initialize());
        Rigidbody = GetComponent<Rigidbody>();
        forcespeed = speed;
        yield return null;
    }


    public override void Tick()
    {
        base.Tick();
        PlatformLeftMove();

    }
   

    private void PlatformLeftMove()
    {
        Rigidbody.velocity = Vector3.left * forcespeed;

        if (timer)
        {
            time += Time.deltaTime;
            if (time > interval)
            {

            
            if (forcespeed > 0)
            {
                forcespeed = -speed;
                timer = false;
                time = 0;
            }
            else if (forcespeed < 0)
            {
                forcespeed = speed;
                timer = false;
                time = 0;
            }

        }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            timer = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 adjustVelocity=new Vector3(this.Rigidbody.velocity.x,this.Rigidbody.velocity.y,this.Rigidbody.velocity.z);
            rb.velocity = adjustVelocity;
        }
    }
   

}
