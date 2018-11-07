using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{

    Rigidbody rbody;
    public float moveSpeed = 5f;

	// Use this for initialization
	void Start ()
    {
        rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetMouseButtonDown(0))
        //{

        //    transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f);
        //}
        //transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        float moveX = inputX * moveSpeed * Time.deltaTime;
        float moveZ = inputZ * moveSpeed * Time.deltaTime;

        rbody.transform.Translate(moveX, 0f, moveZ);
	}
}
