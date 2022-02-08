using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sLimbCollision : MonoBehaviour
{
    //public GameObject rootBody;

    //public sCharacterController characterController;

    private void Start()
    {
        //characterController = GameObject.FindObjectOfType<sCharacterController>().GetComponent<sCharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //sCharacterController.isGrounded = true;
        }
        
    }

}
