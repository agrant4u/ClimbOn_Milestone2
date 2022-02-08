using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sLooseRockBehavior : MonoBehaviour
{

    public float rockFallForce;
    public float rockKnockdownForce;

    public float fallOffset;
    public float fallTime = 0.25f;

    Vector3 offset;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        offset = new Vector3(0, fallOffset, 0);

    }

    private void OnCollisionEnter(Collision collision)
    {

        //sCharacterController player;

        //player = collision.gameObject.GetComponent<sCharacterController>();



        if (collision.gameObject.CompareTag("Player"))
        {

            float elapsedTime = 0f;
           

            Debug.Log("Loose Rock triggered by " + collision.gameObject.name);

            // SHOOTS ROCK OUT
            rb.useGravity = true;
            rb.AddForce(0, 0, -rockFallForce, ForceMode.Impulse);

            // KNOCKS PLAYER DOWN
            collision.gameObject.GetComponent<Rigidbody>().AddForce(0, -rockKnockdownForce, 0, ForceMode.Impulse);
            

            while (elapsedTime < fallTime)
            {
                //collision.gameObject.transform.position = Vector3.Lerp(collision.transform.position, collision.transform.position - offset, (elapsedTime/fallTime));
                //collision.gameObject.transform.rotation = Quaternion.Lerp(collision.transform.rotation, collision.transform.rotation, (elapsedTime / fallTime));
                

                elapsedTime += Time.deltaTime;
            }

            //Vector3 currentPos = player.gameObject.transform.position;
            //Vector3 fallPos = new Vector3(currentPos.x, currentPos.y - fallOffset, currentPos.z);

            //collision.gameObject.transform.position = Vector3.Lerp(currentPos, fallPos, 15f * Time.fixedDeltaTime);

        }

    }

}
