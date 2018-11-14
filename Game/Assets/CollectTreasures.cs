using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTreasures : MonoBehaviour
{
    treasureSetting treasure;
    int playerTreasureID;
    int treasureID = 1;
    string trausureName;

    
	// Use this for initialization
	void Start ()
    {
        playerTreasureID = 1;
        treasure = new treasureSetting();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        treasure.CheckID(treasureID);
        trausureName = "Treasure" + treasureID;
        if (other.gameObject.name == trausureName)
        {
            //treasure.CheckID(treasureID);
            //if (treasureID == playerTreasureID)
            //{
            //    other.gameObject.SetActive(false);
            //}

            other.gameObject.SetActive(false);
            Debug.Log(trausureName);
        }
    }
}
