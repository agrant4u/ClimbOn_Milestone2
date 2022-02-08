using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uStaminaManager : MonoBehaviour
{
    public Image staminaBar;
    public Text staminaText;


    void Start()
    {
        //staminaText.color = Color.black;
        //staminaBar.color = Color.cyan;
        staminaBar.fillAmount = sCharacterController.maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        StaminaBarUpdate();
    }

    public void StaminaBarUpdate()
    {
        float currentStamina = sCharacterController.currentStamina;
        float maxStamina = sCharacterController.maxStamina;

        staminaBar.fillAmount = (currentStamina / maxStamina);

        if (currentStamina <= (maxStamina * 0.1))
            staminaBar.color = Color.red;
        else
        {
            //staminaBar.color = Color.cyan;
        }

    }
}
