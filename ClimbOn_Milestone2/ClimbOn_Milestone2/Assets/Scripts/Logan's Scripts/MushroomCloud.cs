using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class MushroomCloud : MonoBehaviour
{

    
    public ParticleSystem mushroomCloud;
    public sCharacterController characterController;

    public void ActivateMushroom()
    {

        
        //if the master player transform is within activation range, activate the emission of particles, when player leaves, stop emissions.
        if (characterController.masterPlayer.transform)
        {
            //enable particles
        }
        else
        {
            //disable particles
        }
    }

    private void OnParticleCollision(GameObject other)
    {

        //deals damage to player
    }


}
