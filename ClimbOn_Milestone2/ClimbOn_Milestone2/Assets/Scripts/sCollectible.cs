using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCollectible : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {

        sCharacterController player;

        player = collision.gameObject.GetComponent<sCharacterController>();

        if (player)
        {
            AudioManager.am.PlaySFX(eSFX.collectibleGrab);
            sCharacterController.collectiblesHeld++;

            Destroy(this.gameObject);

        }

    }


}
