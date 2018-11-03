using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : MonoBehaviour
{

    Vector3 targetPos;
    Vector3 lookAtTarget;
    Quaternion playerRot;
    private float rotSpeed = 5;
    private float speed = 5;
    private bool moving = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            SetTargetPos();
        }

        if (moving)
            Move();
	}

    void SetTargetPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            targetPos = hit.point;
            //this.transform.LookAt(targetPos);
            lookAtTarget = new Vector3(targetPos.x - transform.position.x, transform.position.y, targetPos.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            moving = true;
        }
    }

    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position == targetPos)
        {
            moving = false;
        }
    }
}
