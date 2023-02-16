using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    public void Grabbed()
    {

        rb.isKinematic = true;
    }
    
    public void Dropped()
    {
        rb.isKinematic = false;
    }
}
