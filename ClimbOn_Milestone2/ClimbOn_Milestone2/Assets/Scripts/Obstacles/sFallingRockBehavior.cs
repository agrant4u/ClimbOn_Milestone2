using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class sFallingRockBehavior : MonoBehaviour
{
    float rockFallPowerMinRange;
    float rockFallPowerMaxRange;

    float scaleMin = 0.0002f;
    float scaleMax = 1f;

    float rockScale;

    float rockFallPower;

    Rigidbody rb;



    private void Start()
    {
        rockScale = Random.Range(scaleMin, scaleMax);

        gameObject.transform.localScale = new Vector3(rockScale, rockScale, rockScale);

        rockFallPower = Random.Range(rockFallPowerMinRange, rockFallPowerMaxRange) * rockScale;

        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }

        sCharacterController player;
        player = collision.gameObject.GetComponent<sCharacterController>();
        if (player)
        {
            AudioManager.am.PlaySFX(eSFX.fallingRockHit);
            //player.gameObject.GetComponent<Rigidbody>().AddForce(rb.velocity * rockFallPower);
            player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * rockFallPower, ForceMode.Acceleration);


        }

    }
}