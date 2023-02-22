using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    //I USED TO USE JOINTS TO ATTACH OBJECTS TO PLAYER
    //BUT IT HAD ISSUES I DIDNT PREDICT: MOVING WITH OBJECT, JITTERY ROTATION ETC..
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
        rb.isKinematic = true;
    }
    
    public void Dropped()
    {
        //playerRb.mass -= rb.mass;
        print("Dropped");
        collider.isTrigger = false;
        rb.isKinematic = false;
    }
}
