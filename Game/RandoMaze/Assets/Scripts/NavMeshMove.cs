using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NavMeshMove : MonoBehaviour
{

    UnityEngine.AI.NavMeshAgent agent;
    Vector3 targetPos;
    
    public bool moving2 = false;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float point = 0f;
            RaycastHit hit;

            if (plane.Raycast(ray, out point));
            {
                targetPos = ray.GetPoint(point);
            }
            moving2 = true;
        }

        if (moving2 == true)
        {
            Move();
        }
    }

    void Move()
    {
        agent.SetDestination(targetPos);
    }
}
