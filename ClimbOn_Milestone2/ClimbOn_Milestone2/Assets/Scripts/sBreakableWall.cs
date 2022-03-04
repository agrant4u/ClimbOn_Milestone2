using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]

public class sBreakableWall : MonoBehaviour
{

    public GameObject brokenWall;

    public float breakVelocityMagnitude;

    //Rigidbody rb;

    Vector3 offset;

    private void Start()
    {

        offset = new Vector3(0, 0, -6.74f);

        //rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {

            if (collision.relativeVelocity.magnitude > breakVelocityMagnitude)
            {

                Debug.Log("Relative Velocity is: " + collision.relativeVelocity.magnitude);

                //Debug.Log("Player velocity is: " + playerRB.velocity.z + " for Z and for X: " + playerRB.velocity.x);

                DestroyWall();
            }

            else
            {
                Debug.Log("Velocity Too Low Only at : " + collision.relativeVelocity.magnitude);
                //Debug.Log("You can't break this! Z Velocity is only: " + playerRB.velocity.z + " for X is: " + playerRB.velocity.x);

            }

            

        }

    }

    void DestroyWall()
    {
        Instantiate(brokenWall, gameObject.transform.position + offset, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
