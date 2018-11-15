using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTreasures : MonoBehaviour
{
    
    List<GameObject> treasureName;
    public EndTurn endPlayerTurn;


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
        //treasure.CheckID(treasureID);
        treasureName = GetComponentInParent<PlayerPieces>().treasures;

        if (other.gameObject == treasureName[0])
        {
            other.gameObject.SetActive(false);
            treasureName.RemoveAt(0);
            endPlayerTurn.buttonPressed = true;
            Debug.Log(treasureName);
        }
    }
}
