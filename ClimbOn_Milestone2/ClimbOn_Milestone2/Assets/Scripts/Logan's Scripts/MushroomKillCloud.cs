using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomKillCloud : MonoBehaviour
{
    public float playerTimeInCloud = 2;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(mushroomEffect());
            Debug.Log("Player being hit by particles");
        }
    }

    IEnumerator mushroomEffect()
    {
        float counter = 0;
        while (counter < playerTimeInCloud)
        {
            if (Mushroom.isInRangeOfMushroom)
            {
                counter += Time.fixedDeltaTime;
                yield return null;
            }
            else
            {
                counter = 0;
                yield return null;
            }
        }
        sCharacterController.isDead = true;

    }

}
