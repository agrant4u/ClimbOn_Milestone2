using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class sFallingFloorBehavior : MonoBehaviour
{

    public float waitTime = 2.5f;

    Rigidbody rb;

    void Start()
    {

        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

    }


    private void OnCollisionEnter(Collision collision)
    {

        sCharacterController player;

        player = collision.gameObject.GetComponent<sCharacterController>();

        if (player)

        {

            // Add floor animation, sounds, etc. here

            StartCoroutine(FloorFall());
            

        }

    }

    IEnumerator FloorFall()
    {
        Debug.Log("Starting Floor Fall");

        yield return new WaitForSeconds(waitTime);

        rb.useGravity = true;

    }

}
