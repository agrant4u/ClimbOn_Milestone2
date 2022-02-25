using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sVineBehavior : MonoBehaviour
{
    sCharacterController player;

    FixedJoint vineJoint;

    Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<sCharacterController>();

            if (sCharacterController.isHoldingVine)
            {
                return;
            }

            else
            {
                sCharacterController.isHoldingVine = true;

                player.VineHold(this.gameObject);

                vineJoint = gameObject.AddComponent<FixedJoint>();
                vineJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();

            }

        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sCharacterController.isHoldingVine = true;
        }

       

    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            //sCharacterController.isHoldingVine = false;
        }

    }

}
