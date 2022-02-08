using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sGoatBehavior : MonoBehaviour
{

    public float buckForce = 5f;

    sCharacterController player;

    public int countDown=5;
    
    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject.GetComponent<sCharacterController>();

        if (!sCharacterController.isGettingBucked && player)
        {

            Debug.Log("Goat is launching" + collision.gameObject.name);
            player.gameObject.GetComponent<Rigidbody>().AddForce(0, buckForce, 0, ForceMode.Impulse);

            sCharacterController.isGettingBucked = true;

            StartCoroutine("BuckCountdown");

        }

    }

    // WRITE COROUTINE FOR COOLDOWN
    IEnumerator BuckCountdown()
    {

        for (int i = 0; i < countDown; i++)
        {
            Debug.Log("BuckCountdown has waited " + i + "seconds.");
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Player is now buckable");
        sCharacterController.isGettingBucked = false;
    }
   
}
