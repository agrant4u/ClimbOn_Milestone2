using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Mushroom : MonoBehaviour
{

    public GameObject mushroomCloud;
    ParticleSystem mushroomParticle;
    sCharacterController characterController;
    MushroomKillCloud mushroomBehavior;
    public float playerTimeInCloud = 10;

    public static bool isInRangeOfMushroom;
    


    private void Start()
    {
        mushroomParticle = mushroomCloud.GetComponent<ParticleSystem>();
       //mushroomBehavior = mushroomCloud.GetComponent<MushroomKillCloud>();
    }




    private void OnTriggerEnter(Collider player)
    {
        
        if (player.gameObject.CompareTag("Player"))
        {
            Mushroom.isInRangeOfMushroom = true;
            mushroomParticle.Play();
            StartCoroutine(mushroomEffect());
            Debug.Log("Player being hit by particles");
            Debug.Log("Player is in Range of mushroom cloud");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Mushroom.isInRangeOfMushroom = true;
        //mushroomParticle.Play();
        //StartCoroutine(mushroomEffect());
        Debug.Log("Player being hit by particles");
        Debug.Log("Player is in Range of mushroom cloud");
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Mushroom.isInRangeOfMushroom = false;
            mushroomParticle.Stop();

            Debug.Log("Player is not in Range of mushroom cloud");
        }
    }


    IEnumerator mushroomEffect()
    {
        float counter = 0;

        while (counter < playerTimeInCloud)
        {
            if (Mushroom.isInRangeOfMushroom)
            {
                counter += Time.deltaTime;
                yield return null;
            }
        }
        sCharacterController.isDead = true;

    }

}
