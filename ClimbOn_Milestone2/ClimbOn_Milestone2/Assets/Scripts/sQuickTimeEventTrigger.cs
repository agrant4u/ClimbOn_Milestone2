using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sQuickTimeEventTrigger : MonoBehaviour
{

    public GameObject displayBox;
    public GameObject passBox;

    public int qTEGen;
    public int waitingForKey;
    public int correctKey;
    public int countingDown;

    public Text buttonPress;

    private void OnCollisionEnter(Collision collision)
    {

        sCharacterController player;

        player = collision.gameObject.GetComponent<sCharacterController>();

        if (player)
        {

            StartQTE();

        }

    }

    public void StartQTE()
    {



    }

    void Update()
    {
    
        if (waitingForKey == 0)
        {

            qTEGen = Random.Range(1, 4);
            countingDown = 1;

            StartCoroutine(CountDown());

            if (qTEGen == 1)
            {

                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[E]";

            }

            if (qTEGen == 2)
            {

                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[R]";

            }

            if (qTEGen == 3)
            {

                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[T]";

            }

        }

        if (qTEGen == 1)
        {

            // check key press here
            if (Input.anyKeyDown)
            {
                // check key sequence of inputs here with another if

                correctKey = 1;

                StartCoroutine(KeyPressing());
            }

            else
            {

                correctKey = 2;

                StartCoroutine(KeyPressing());

            }

        }

        if (qTEGen == 2)
        {

            // check key press here
            if (Input.anyKeyDown)
            {
                // check key sequence of inputs here with another if

                correctKey = 1;

                StartCoroutine(KeyPressing());
            }

            else
            {

                correctKey = 2;

                StartCoroutine(KeyPressing());

            }

        }

        if (qTEGen == 3)
        {

            // check key press here
            if (Input.anyKeyDown)
            {
                // check key sequence of inputs here with another if

                correctKey = 1;

                StartCoroutine(KeyPressing());
            }

            else
            {

                correctKey = 2;

                StartCoroutine(KeyPressing());

            }

        }



    }

    IEnumerator KeyPressing()
    {

        qTEGen = 4;

        if (correctKey == 1)
        {

            countingDown = 2;
            passBox.GetComponent<Text>().text = "Pass";
             
            yield return new WaitForSeconds(1.5f);

            correctKey = 0;

            passBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text = "";

            yield return new WaitForSeconds(1.5f);

            waitingForKey = 0;
            countingDown = 1;

        }

        if (correctKey == 2)
        {

            countingDown = 2;
            passBox.GetComponent<Text>().text = "Fail";

            yield return new WaitForSeconds(1.5f);

            correctKey = 0;

            passBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text = "";

            yield return new WaitForSeconds(1.5f);

            waitingForKey = 0;
            countingDown = 1;

        }



    }

    IEnumerator CountDown()
    {

        yield return new WaitForSeconds(3.5f);

        if (countingDown == 1)
        {

            qTEGen = 4;
            countingDown = 2;

            passBox.GetComponent<Text>().text = "Fail";
            yield return new WaitForSeconds(1.5f);
            correctKey = 0;
            passBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.5f);
            waitingForKey = 0;
            countingDown = 1;

        }

    }


}
