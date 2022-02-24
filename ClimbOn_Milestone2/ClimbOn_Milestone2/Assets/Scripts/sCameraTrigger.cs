using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public enum eCameraState { topRig, middleRig, bottomRig }

public class sCameraTrigger : MonoBehaviour
{

    public GameObject pFreeLookObject;

    bool isTriggered;

    CinemachineFreeLook freeLookCam;

    float rigAxis;

    public eCameraState camState;

    public float transitionTime = 2f;


    private void Awake()
    {
        isTriggered = false;
        freeLookCam = pFreeLookObject.GetComponent<CinemachineFreeLook>();

        SetYAxis();

    }


    private void OnTriggerEnter(Collider other)
    {

        sCharacterController player;
        player = other.gameObject.GetComponent<sCharacterController>();

        if (player && !isTriggered)

        {
            Debug.Log("Triggering Camera collier");
            isTriggered = true;
            StartCoroutine(CameraSwitch(rigAxis));

        }
        


    }

    void SetYAxis()
    {

        switch(camState)
        {
            case eCameraState.bottomRig:
                rigAxis = 0;

                break;

            case eCameraState.middleRig:
                rigAxis = 0.5f;

                break;

            case eCameraState.topRig:
                rigAxis = 1f;

                break;

        }

    }


    IEnumerator CameraSwitch(float _rigAxis)
    {

        float currentValue = freeLookCam.m_YAxis.Value;
        float counter = 0;

        //interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;
        while(counter<transitionTime)
        {
            freeLookCam.m_YAxis.Value = Mathf.Lerp(currentValue, _rigAxis, (counter/transitionTime));

            Debug.Log("Moving Camera from " + currentValue + " to " + _rigAxis);

            counter += Time.deltaTime;

            yield return null;
        }
        

        //Destroy(this.gameObject);
       
    }

}
