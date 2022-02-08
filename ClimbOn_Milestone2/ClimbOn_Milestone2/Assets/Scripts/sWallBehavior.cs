using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum slowDownStatus { LOW, MEDIUM, HIGH, NONE }

public class sWallBehavior : MonoBehaviour
{
    public int slip = 20;

    float lowSlowdownAmount = 1.5f;
    float mediumSlowdownAmount = 2f;
    float highSlowdownAmount = 3f;
    float noSlowdownAmount = 1f;

    float currentSlowdownAmount;

    public slowDownStatus currentSlowDownState;

    // CHECKS FOR SLOWDOWN STATE ON WALL AND RUTURNS SLOWDOWN AMOUNT
    public float CheckSlowDownState()
    {
        switch (currentSlowDownState)

        {

            case slowDownStatus.LOW:
                {
                    currentSlowdownAmount = lowSlowdownAmount;
                    break;
                }
            case slowDownStatus.MEDIUM:
                {
                    currentSlowdownAmount = mediumSlowdownAmount;
                    break;
                }
            case slowDownStatus.HIGH:
                {
                    currentSlowdownAmount = highSlowdownAmount;
                    break;
                }
            case slowDownStatus.NONE:
                {
                    currentSlowdownAmount = noSlowdownAmount;
                    break;
                }

        }

        return currentSlowdownAmount;

    }

            

}
