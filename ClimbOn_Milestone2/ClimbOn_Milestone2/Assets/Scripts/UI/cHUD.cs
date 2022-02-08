using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cHUD : MonoBehaviour
{
    public float startingTime;

    public Text textTime;

    public Text collectiblesNumber;

    private void Start()
    {
        startingTime = Time.time;
        textTime.text = startingTime.ToString();



    }

    void Update()
    {

        ClockUpdate();

        CollectibleUpdate();

    }

    void ClockUpdate()
    {


        float t = Time.time - startingTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");// f2 indicates how many floats we want timer to show

        textTime.text = minutes + ":" + seconds;

    }

    void CollectibleUpdate()
    {

        collectiblesNumber.text = sCharacterController.collectiblesHeld.ToString() + "/" + sCharacterController.totalCollectibles;

    }

}
