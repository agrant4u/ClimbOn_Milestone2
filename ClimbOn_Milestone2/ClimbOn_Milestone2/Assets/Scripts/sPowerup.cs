using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum powerupType { SPEED_BURST, STAMINA_BOOST, STAMINA_BUFF}

public class sPowerup : MonoBehaviour
{

    public float speedBoostTime = 10f;
    public float speedBoostAmount = 10f;

    public float staminaBoostAmount = 50f;

    public powerupType currentPowerupType;

    sCharacterController player;

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

        player.SpeedBurst(speedBoostAmount, speedBoostTime);

        PowerupDestroy();

    }


    void StaminaBoost()
    {

        player.StaminaChange(staminaBoostAmount);

        PowerupDestroy();

    }

    void StaminaBuff()
    {



    }

    void PowerupDestroy()
    {

        Destroy(this.gameObject);

    }

}
