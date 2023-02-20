using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    //public Rigidbody playerRb;
    public Rigidbody rb;
    public Collider collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void Grabbed()
    {
        //playerRb.mass += rb.mass;
        print("Grabbed");
        collider.isTrigger = true;
    }
    
    public void Dropped()
    {
        //playerRb.mass -= rb.mass;
        print("Dropped");
        collider.isTrigger = false;
    }
}
