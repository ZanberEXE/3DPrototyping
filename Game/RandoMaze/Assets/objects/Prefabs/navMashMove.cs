using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class navMashMove : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent agent;
    private Vector3 clickPos;
    public Vector3 targetPos;
    public bool moving2 = false;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //MousePosCheck();

        if (Input.GetMouseButton(0)&&moving2)
        {
            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float point = 0f;

            if (plane.Raycast(ray, out point))
            {
                targetPos = ray.GetPoint(point);
            }
            Move();
        }else if (!moving2)
        {
            agent.SetDestination(GetComponentInParent<Transform>().position);
        }
        
	}

    void Move()
    {
        agent.SetDestination(targetPos);
        //Debug.DrawLine(transform.position, targetPos, Color.black);
    }

    void MousePosCheck()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPos = hit.point;
            }
            Debug.Log(targetPos);
        }
    }
}
