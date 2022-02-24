using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sUiNavigation : MonoBehaviour
{
    public GameObject cOptions;

    public GameObject panelOptions;

    GameManager gm;


    private void Start()
    {

        gm = GameManager.gm;

    }

    public void OnPlayButtonPressed()
    {

        gm.LoadScene(eScene.inGame);

        Destroy(this.gameObject);

    }

    public void OnOptionsPressed()
    {

        Instantiate(cOptions, panelOptions.transform);
        Debug.Log("Options here!");

    }

    public void OnQuitPressed()
    {

        Application.Quit();

    }






}
