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
    public bool waiting = false;
    public float delay = 0.2f;
    public bool moving2 = false;
    Ray ray;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        //MousePosCheck();
        if (waiting)
        {
            startTimer();
        }
        else
        {
            if (Input.GetMouseButton(0) && moving2)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, transform.position);
                float point = 0f;
                if (plane.Raycast(ray, out point))
                {
                    targetPos = ray.GetPoint(point);
                }
                startTimer();


            }
            else if (!moving2 && agent.isActiveAndEnabled)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }
        }
        
	}

    public void startTimer()
    {
        if (!waiting)
        {
            waiting = true;

        }
        else
        {
            if (delay > Time.deltaTime)
                delay -= Time.deltaTime;
            else
            {
                delay=0.2f;
                waiting = false;
                startMoving();
            }
        }
    }
    public void startMoving()
    {
        Move();
    }
    public void Move()
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
