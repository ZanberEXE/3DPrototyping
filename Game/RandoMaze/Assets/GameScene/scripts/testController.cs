using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : MonoBehaviour
{

    Rigidbody rigidbody;
    Vector3 targetPos;
    Vector3 lookAtTarget;
    Vector3 clickPos;
    Quaternion playerRot;
    public GameObject point;
    private float rotSpeed = 5;
    public float speed = 5;
    private bool moving = false;


    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    SetTargetPos();
        //}

        //if (moving)
        //    Move();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    targetPos.y = transform.position.y;
        //    if (moving == false)
        //    {
        //        moving = true;
        //    }

        //    Instantiate(point, targetPos, Quaternion.identity);
        //}

        //if (moving == true)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        //}

        if (Input.GetMouseButtonDown(0))
        {

            /// works!

            //clickPos = -Vector3.one;

            //clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));


            //moving = true;
            //Debug.Log(clickPos);

            /// works too!
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPos = hit.point;
            }

            moving = true;
            Debug.Log(clickPos);
        }

        if (moving == true)
        {
            Move(clickPos);
        }
    }

    void SetTargetPos()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, 1000))
        //{
        //    targetPos = hit.point;
        //    //this.transform.LookAt(targetPos);
        //    lookAtTarget = new Vector3(targetPos.x - transform.position.x, transform.position.y, targetPos.z - transform.position.z);
        //    //playerRot = Quaternion.LookRotation(lookAtTarget);
        //    moving = true;
        //}
    }

    void Move(Vector3 targetPos)
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        //if (transform.position == targetPos)
        //{
        //    moving = false;
        //}

        targetPos.y = -2f;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        //rigidbody.transform.Translate(targetPos);

    }
}
