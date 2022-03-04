using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sVineSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject partPrefab, parentObject;

    [SerializeField]
    [Range(1,1000)]
    int length = 1;

    [SerializeField]
    float partDistance = 0.21f;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;

    //HingeJoint joint;

    private void Update()
    {
        
        if(reset)
        {

            foreach(GameObject tmp in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(tmp);
            }

            reset = false;

        }


        if (spawn)
        {
            Spawn();

            spawn = false;
        }


    }

    void Spawn()
    {

        int count = (int)(length / partDistance);

        for (int i = 0; i < count; i++)
        {
            GameObject tmp;

            tmp = Instantiate(partPrefab, new Vector3(transform.position.x, parentObject.transform.position.y * partDistance * (-i + 1), transform.position.z), Quaternion.identity, parentObject.transform);
            tmp.transform.eulerAngles = new Vector3(180, 0, 0);

            tmp.name = parentObject.transform.childCount.ToString();
            
            if(i==0)
            {
                //Destroy(tmp.GetComponent<CharacterJoint>());
                tmp.GetComponent<CharacterJoint>().connectedBody = parentObject.GetComponent<Rigidbody>();

            }

            else
            {

                tmp.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();

            }
        }
    }

}
