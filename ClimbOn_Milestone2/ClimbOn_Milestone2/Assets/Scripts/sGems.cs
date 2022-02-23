using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sGems : MonoBehaviour
{

    public int gemValue;

    

    private void OnCollisionEnter(Collision collision)
    {

        sCharacterController player;

        player = collision.gameObject.GetComponent<sCharacterController>();

        if (player)
        {

            sCharacterController.gemsHeld+=gemValue;

            

            Destroy(this.gameObject);

        }

    }






}
