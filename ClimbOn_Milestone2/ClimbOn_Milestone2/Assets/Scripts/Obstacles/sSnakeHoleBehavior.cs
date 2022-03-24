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

    float attackSpeed = 500f;

    //int attackXmin = -6;
   // int attackYmin = -6;
   // int attackXmax = 6;
   // int attackYmax = 6;

    int attackX;
    int attackY;

    public float offsetAmount=4;

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

    private void Update()
    {
        
        if (hasSnaked && snake)
        {
            SnakeBehavior();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        // COLLISION FOR SNAKE WITH PLAYER
        sCharacterController player;
        player = other.gameObject.GetComponent<sCharacterController>();

        if (player && !hasSnaked)
        {
            hasSnaked = true;
            AudioManager.am.PlaySFX(eSFX.snakeTrigger);

            snake = Instantiate(snakeBody, this.gameObject.transform.position - offset, this.gameObject.transform.rotation);

        }

    }

    void SnakeBehavior()
    {

        switch (snakeType)
        {

            case eTypeOfSnake.angry:

                GoAngrySnake();
                break;

            case eTypeOfSnake.anxious:

                GoAnxiousSnake();
                break;

        }

    }

    void GoAngrySnake()
    {
        while(attackX == 0)
        {
            GetRandomAttackSpot();
        }
       
        //attackX = Random.Range(-1, 2);

        attackDirection = new Vector3(attackX, 0, 0);

        //snakeAnimator.SetBool("isSnakeMoving", true);

        //snake.GetComponent<Rigidbody>().AddForce(attackDirection * attackSpeed - offset);

        //snake.GetComponent<Rigidbody>().velocity += attackDirection * attackSpeed;

        //player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 1000f);
    }

    void GoAnxiousSnake()
    {

    }

    

    void GetRandomAttackSpot()
    {
        attackX = Random.Range(-1, 2);
        //attackY = Random.Range(-1, 2);

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
