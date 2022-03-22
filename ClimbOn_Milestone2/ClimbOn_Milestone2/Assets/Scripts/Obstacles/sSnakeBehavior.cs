using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class sSnakeBehavior : MonoBehaviour
{
    Rigidbody rb;

    Vector3 movementDir;
    
    float movementSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

        movementDir = new Vector3(movementSpeed, 0, 0);

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        movementDir = new Vector3(movementSpeed, 0, 0);

        rb.MovePosition(movementDir);

        movementSpeed+=Time.deltaTime * 4;

        //rb.position += movementDir;

       // rb.velocity += movementDir;
    }
}
