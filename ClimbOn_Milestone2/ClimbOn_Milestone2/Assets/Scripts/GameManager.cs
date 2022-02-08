using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public enum eScene { mainMenu, inGame }


public class GameManager : MonoBehaviour
{

    public static GameManager gm;


    [Space]
    [Header("Scene Management")]
    int currentScene;
    public eScene curScene;
    int startScene = 0;



    private void Awake()
    {

        if (gm == null)
        {

            DontDestroyOnLoad(this.gameObject);
            gm = this;

        }

        else if (gm != this)
        {

            Destroy(gameObject);

        }
    }


        void Start()
        {


        if (curScene == eScene.mainMenu)
        {
            

            
        }

        if (curScene == eScene.inGame)
        {



        }


  }
    



    public void LoadScene(eScene _sceneToLoad)
    {

        //curScene = _newScene;

        //Debug.Log("sceneName to load: " + _newScene);

        SceneManager.LoadScene((int)_sceneToLoad);


    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        curScene = (eScene)currentScene;

        Debug.Log("Scene has loaded: " + curScene);

        switch (curScene)
        {
            case eScene.mainMenu:

                //CreateMainMenu();

                break;

            case eScene.inGame:

                break;

                // CAMERA SETUP
        }
 
    }




}
