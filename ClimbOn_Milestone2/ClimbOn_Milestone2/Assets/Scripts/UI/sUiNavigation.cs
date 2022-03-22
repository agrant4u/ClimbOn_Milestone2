using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class sUiNavigation : MonoBehaviour
{
    public GameObject cOptions;

    public GameObject panelOptions;

    GameManager gm;

    public GameObject pauseFirstButton, menuOptionsFirstButton, menuOptionsCloseButton,mainMenuFirstButton;

    private void Start()
    {

        gm = GameManager.gm;

    }

    public void OnPlayButtonPressed()
    {

         gm.LoadScene(eScene.inGame);
      //  sLevelManager.Instance.LoadScene(sceneName);
        Destroy(this.gameObject);

    }

    public void OnOptionsPressed()
    {

        Instantiate(cOptions, panelOptions.transform);
        //clears the selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(menuOptionsFirstButton);
        Debug.Log("Options here!");

    }

    public void OnQuitPressed()
    {

        Application.Quit();

    }






}
