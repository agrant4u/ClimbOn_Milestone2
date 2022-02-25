using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sSpikeBehavior : MonoBehaviour
{
    public bool instantKill = true;

    private void OnCollisionEnter(Collision collision)
    {

        sCharacterController player;

        player = collision.gameObject.GetComponent<sCharacterController>();

        if(player && instantKill)
        {

            sCharacterController.isDead = true;

        }

    }

}
