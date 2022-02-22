using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eTypeOfSnake { angry, anxious }

public class sSnakeHoleBehavior : MonoBehaviour
{

    public GameObject snakeBody;
    Animator snakeAnimator;

    public int snakeAnimationCountDown = 5;  // SNAKE COOLDOWN TIME

    Vector3 attackDirection;
    Vector3 offset;

    float attackSpeed = 20f;

    float attackXmin = -6f;
    float attackYmin = -6f;
    float attackXmax = 6f;
    float attackYmax = 6f;

    float attackX;
    float attackY;

    public float offsetAmount=2;

    Rigidbody rb;

    GameObject snake;

    bool hasSnaked;

    public eTypeOfSnake snakeType;

    private void Start()
    {

        hasSnaked = false;
        snakeAnimator = snakeBody.GetComponent<Animator>();

        attackDirection = new Vector3(attackX, attackY, 0);
        offset = new Vector3(0, 0, offsetAmount);
        //rb = snakeBody.GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {

        // COLLISION FOR SNAKE WITH PLAYER
        sCharacterController player;
        player = collision.gameObject.GetComponent<sCharacterController>();

        if (player && !hasSnaked)
        {

            //snakeAnimator.SetBool("isSnakeMoving", true);

            snake = Instantiate(snakeBody, this.gameObject.transform.position, Quaternion.identity);
            GetRandomAttackSpot();
            snake.GetComponent<Rigidbody>().AddForce(attackDirection*attackSpeed - offset);

            //hasSnaked = true;

            player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 1000f);

        }    
    }

    

    void GetRandomAttackSpot()
    {
        attackX = Random.Range(attackXmin, attackXmax);
        attackY = Random.Range(attackYmin, attackYmax);

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
