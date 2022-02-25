using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sUpdraftBehavior : MonoBehaviour
{
    public float updraftPower;

    private void OnTriggerEnter(Collider other)
    {
        sCharacterController player;

        player = other.gameObject.GetComponent<sCharacterController>();

        if (player)
        {

            player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * updraftPower, ForceMode.Force);

        }

    }

}
