using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCheckPointBehavior : MonoBehaviour
{
    private bool isUsed = false;

    public GameObject flag;


    private void OnCollisionEnter(Collision collision)
    {

        sCharacterController player;

        player = collision.gameObject.GetComponent<sCharacterController>();

        if (player  && !isUsed)
        {
            player.SetNewCheckPoint(this.gameObject.transform.position);

            //flag.GetComponent<Material>().color = Color.green;


            isUsed = true;

        }

    }

}
