using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class MushroomCloud : MonoBehaviour
{

    public ParticleSystem mushroomCloud;
    public sCharacterController characterController;

    public static bool isInRange;
    public int playerTimeInCloud;
    


    private void Start()
    {
        playerTimeInCloud = 0;
    }

    private void OnCollisionEnter(Collision player)
    {
        
        if (player.gameObject.CompareTag("Player"))
        {
            MushroomCloud.isInRange = true;
            mushroomCloud.Play();
            
            Debug.Log("Player is in Range of mushroom cloud");
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            MushroomCloud.isInRange = true;
            mushroomCloud.Play();

            Debug.Log("Player is in Range of mushroom cloud");
        }
    }

    

    private void OnParticleCollision(GameObject other)
    {
        
        
        if (playerTimeInCloud == 5)
        {
            //StartCoroutine(mushroomEffect);
            //this will kill player
        }
        //if playerTimeInCloud <= 5, then have the player time in cloud be reset to 0
        
            //
        
        //StopCoroutine(mushroomEffect);
    }

    IEnumerator mushroomEffect()
    {
        int playerLife;
        playerLife = 0;
        return null;
    }

    
}
