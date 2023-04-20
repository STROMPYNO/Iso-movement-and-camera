using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour
{
    public Transform orientation;
    public Rigidbody rb;

    public float dashForce = 5;
    public ParticleSystem particle;
  

   public  bool useParticle;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public void Dash()
    {
        if (useParticle)
        {
            particle.Play();
        }
       
        rb.AddForce(orientation.transform.forward * dashForce, ForceMode.Impulse);

      
      
    }

   
}
