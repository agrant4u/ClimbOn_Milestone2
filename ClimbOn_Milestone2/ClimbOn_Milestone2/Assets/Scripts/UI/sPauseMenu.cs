using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sPauseMenu : MonoBehaviour
{

    public GameObject pauseFirstButton;


    void OnAwake()
    {

        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

    }


    


}
