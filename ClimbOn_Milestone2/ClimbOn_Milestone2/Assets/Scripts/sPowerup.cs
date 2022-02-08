using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum powerupType { SPEED_BURST, STAMINA_BOOST, STAMINA_BUFF}

public class sPowerup : MonoBehaviour
{
    public float speedBoostTime = 0;
    public float speedBoostAmount = 10f;

    public float staminaBoostAmount = 50f;

    public powerupType currentPowerupType;

    sCharacterController player;

    float startingClimbSpeed;
    float startingWalkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject.GetComponent<sCharacterController>();

        if (player)
        {

            InitPowerup();

        }



    }


    void InitPowerup()
    {

        switch(currentPowerupType)
        {

            case powerupType.SPEED_BURST:

                SpeedBurst();

                break;

            case powerupType.STAMINA_BOOST:

                StaminaBoost();

                break;

            case powerupType.STAMINA_BUFF:

                StaminaBuff();

                break;


        }

        

    }

    void SpeedBurst()
    {
        player.SpeedBurst(speedBoostAmount);

        Destroy(this.gameObject);

    }


    void StaminaBoost()
    {

        player.StaminaChange(staminaBoostAmount);

        Destroy(this.gameObject);

    }

    void StaminaBuff()
    {



    }

    void PowerupDestroy()
    {

        Destroy(gameObject);

    }

}
