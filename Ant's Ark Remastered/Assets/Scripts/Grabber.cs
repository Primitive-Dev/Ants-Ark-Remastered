using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public bool inGrabRange;
    public Grabbable grabbableInReach;

    public bool holdingSomething;
    public Grabbable heldGrabbable;

    public FixedJoint joint;
    public float rotationSpeed;

    void Update()
    {
        HandleRotation();
        MyInput();
    }

    private void HandleRotation()
    {
        transform.Rotate(-Input.GetAxis("Mouse Y") * rotationSpeed, 0, 0);
    }

    private void MyInput()
    {
        // when to Grab
        if (Input.GetMouseButtonDown(0))
        {
            //RefreshGrab();

            if (!holdingSomething && grabbableInReach)
            {
                Grab();
            }
            else if(holdingSomething)
            {
                Drop();
            }
        }
    }

    private void RefreshGrab()
    {
        if(heldGrabbable == null)
        {
            holdingSomething = false;
        }
        else
        {
            holdingSomething = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!holdingSomething && other.GetComponent<Grabbable>())
        {
            
            inGrabRange = true;
            grabbableInReach = other.gameObject.GetComponent<Grabbable>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        inGrabRange = false;
        grabbableInReach = null;
       
    }

    public void Grab()
    {
        heldGrabbable = grabbableInReach;
        joint.connectedBody = heldGrabbable.GetComponent<Rigidbody>();
    


        //heldGrabbable.transform.SetParent(transform);
        //heldGrabbable.Grabbed();
        holdingSomething = true;
    }

    public void Drop()
    {
        //heldGrabbable.Dropped();
        //heldGrabbable.transform.SetParent(null);

        joint.connectedBody = null;

        heldGrabbable = null;
        holdingSomething = false;
    }
}
