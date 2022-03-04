using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))]
public class sCamConfiner : MonoBehaviour
{
    CinemachineConfiner confiner;

    sCharacterController player;

    BoxCollider confineTrigger;

    //MeshCollider boundingCollider;

    private void Awake()
    {
        
        confineTrigger = gameObject.GetComponent<BoxCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("Cam Confiner triggered");

            player = other.gameObject.GetComponent<sCharacterController>();

            confiner = player.camController.GetComponent<CinemachineConfiner>();

            confiner.m_BoundingVolume = confineTrigger;

        }


    }

    
    
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("Cam Confiner triggered");

            player = other.gameObject.GetComponent<sCharacterController>();

            confiner = player.camController.GetComponent<CinemachineConfiner>();

            confiner.m_BoundingVolume = confineTrigger;

        }


    }

    


    private void OnTriggerExit(Collider other)
    {

        if (confiner)

        {
            confiner.m_BoundingVolume = null;
        }


    }


}
