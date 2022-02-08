using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sFallingRockBehavior : MonoBehaviour
{
    public int rockFallPower = 100;

    private void OnCollisionEnter(Collision collision)
    {

        sCharacterController player;
        player = collision.gameObject.GetComponent<sCharacterController>();
        if (player)
        {

            player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down,ForceMode.Acceleration);


        }

    }
}
