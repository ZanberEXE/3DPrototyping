using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMove : MonoBehaviour
{


    public float moveSpeed = 5;
    public bool moving = false;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            float moveX = inputX * moveSpeed * Time.deltaTime;
            float moveZ = inputZ * moveSpeed * Time.deltaTime;

            transform.Translate(moveX, 0f, moveZ);
        }
    }
}