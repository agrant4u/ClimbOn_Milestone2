using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sSnakeHoleBehavior : MonoBehaviour
{

    public GameObject snakeBody;
    Animator snakeAnimator;

    public int snakeAnimationCountDown = 5;  // SNAKE COOLDOWN TIME

    private void Start()
    {

        snakeAnimator = snakeBody.GetComponent<Animator>();
        

    }

    private void OnCollisionEnter(Collision collision)
    {


        // COLLISION FOR SNAKE WITH PLAYER
        sCharacterController player;
        player = collision.gameObject.GetComponent<sCharacterController>();

        if (player)
        {

            snakeAnimator.SetBool("isSnakeMoving", true);


        }    
    }

    IEnumerator SnakeCountdown()
    {

        for (int i = 0; i < snakeAnimationCountDown; i++)
        {
            Debug.Log("Snake Countdown has waited " + i + "seconds.");
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Player is now snake-able");
        snakeAnimator.SetBool("isSnakeMoving", false);
    }

}
