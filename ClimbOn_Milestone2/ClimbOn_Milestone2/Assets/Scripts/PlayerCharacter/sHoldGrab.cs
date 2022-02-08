using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sHoldGrab : MonoBehaviour
{

    // PUT THIS SCRIPT ON THE HANDS AND FEET GAMEOBJECTS

    public GameObject handHold;

    public float limbBreakForce;

    Rigidbody rb;

    void Start()
    {
        //rb = GetComponent<Rigidbody>(); // rigid body of hold

    }


    public void OnTriggerEnter(Collider collision)
    {
        // GRAB HOLD METHOD

        if(!gameObject.GetComponent<FixedJoint>())
        {
            if (collision.gameObject.CompareTag("Hold"))
            {
                Debug.Log("Player collision with hold");
                handHold = collision.gameObject;

                rb = handHold.GetComponent<Rigidbody>();

                //FixedJoint fj = handHold.AddComponent<FixedJoint>();
                FixedJoint fj = gameObject.AddComponent<FixedJoint>();
                //sCharacterController.fixedJoints.Add(fj);

                // connects person to hold with fixed joint

                fj.connectedBody = rb;
                fj.breakForce = limbBreakForce;  // break force for joint.  Use for stamina?
            }
        }

        
    }

    public void OnTriggerExit(Collider other)
    {

        handHold = null;

    }
}
