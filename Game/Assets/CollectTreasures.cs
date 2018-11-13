using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTreasures : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("treasure 1"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
