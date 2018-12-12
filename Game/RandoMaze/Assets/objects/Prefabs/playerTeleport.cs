using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class playerTeleport : MonoBehaviour {
    public string direction;
    public int multiplier;
	private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Debug.Log(other.transform.position);
            other.GetComponent<Rigidbody>().Sleep();
            if (direction == "x")
            {
                other.GetComponent<NavMeshAgent>().Warp(new Vector3(9 * multiplier, other.transform.position.y, other.transform.position.z));
            }else if (direction == "z")
            {
                other.GetComponent<NavMeshAgent>().Warp(new Vector3(other.transform.position.x, other.transform.position.y, 9 * multiplier));
            }
            other.GetComponent<Rigidbody>().WakeUp();
            Debug.Log("Teleport");
            Debug.Log(other.transform.position);
        }
    }
}
