using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTreasures : MonoBehaviour
{
    
    GameObject trausureName;
    public EndTurn turn;
    
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

        if (other.tag != "goal")
        {
            trausureName = GetComponentInParent<PlayerPieces>().treasures[0];

            if (other.gameObject == trausureName)
            {
                //treasure.CheckID(treasureID);
                //if (treasureID == playerTreasureID)
                //{
                //    other.gameObject.SetActive(false);
                //}

                turn.buttonPressed = true;

                //other.gameObject.SetActive(false);
                Debug.Log(trausureName);
            }
        }
        else if(GetComponentInParent<PlayerPieces>().treasures[0].tag=="goal")
        {
            GetComponentInParent<PlayerPieces>().reachedGoal = true; 
        }
    }
}
