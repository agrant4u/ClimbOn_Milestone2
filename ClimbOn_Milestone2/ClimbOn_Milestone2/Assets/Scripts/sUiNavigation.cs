using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sUiNavigation : MonoBehaviour
{

    GameManager gm;


    private void Start()
    {

        gm = GameManager.gm;

    }

    public void OnPlayButtonPressed()
    {

        //add a move to new scene code
        

        gm.LoadScene(eScene.inGame);

        Destroy(this.gameObject);

    }

    public void OnOptionsPressed()
    {

        Debug.Log("Options here!");

    }

    public void OnQuitPressed()
    {

        Application.Quit();

    }






}
