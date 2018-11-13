using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class navMashMove : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent agent;
    Vector3 targetPos;
    bool outOfPlane = true;
    public bool moving2 = false;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MouseDetect();
        //MouseClickCheck();
        if (outOfPlane == true)
        {


            if (Input.GetMouseButton(0))
            {
                Plane plane = new Plane(Vector3.up, transform.position);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float point = 10.0f;

                if (plane.Raycast(ray, out point))
                {
                    targetPos = ray.GetPoint(point);
                }
                if (moving2 == true)
                {


                    Move();
                }
            }

        }
        
	}

    void Move()
    {
        agent.SetDestination(targetPos);
        //Debug.DrawLine(transform.position, targetPos, Color.black);
    }

    void MouseClickCheck()
    {
        if (Input.mousePosition.x < 9.5 && Input.mousePosition.z < 3.3)
        {
            outOfPlane = false;
        }
        else
        {
            outOfPlane = true;
        }
    }

    void MouseDetect()
    {
        Debug.Log(targetPos);
    }
}
