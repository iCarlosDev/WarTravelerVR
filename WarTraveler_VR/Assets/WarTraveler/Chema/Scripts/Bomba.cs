using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{

    private int numbarcos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barco"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        
       if  (other.gameObject.CompareTag("Mar"))
        {
            Destroy(gameObject);
            
        }
       
        
        
    }
}
