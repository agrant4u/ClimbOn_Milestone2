using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    public int keyCount;
    public int maxKeys = 5;
    public Collision objectiveCollision;


    private sCharacterController characterController;

    public void IncrementKeyCount()
    {
        keyCount++;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (objectiveCollision != null && objectiveCollision.gameObject.tag == "Player")
        {
            IncrementKeyCount();

            
        }
        this.objectiveCollision = collision;
    }

    void OnCollisionExit(Collision collision)
    {
        this.objectiveCollision = null;
    }
}
