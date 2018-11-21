using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTreasures : MonoBehaviour
{
    
    
    public EndTurn turn;
    public bool walkingIntoTreasure = false;
    public bool walked = false;
    public float distanceToMiddle;
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (GetComponentInParent<PlayerPieces>().treasures.Count != 0)
        {
            if (other.gameObject == GetComponentInParent<PlayerPieces>().treasures[0])
            {
                distanceToMiddle = Vector2.Distance(new Vector2(other.bounds.center.x, other.bounds.center.z), new Vector2(transform.position.x, transform.position.z));
                if (other.tag == "goal")
                {
                    GetComponentInParent<PlayerPieces>().reachedGoal = true;
                }else if (other.tag == "treasure")
                {
                    if (!walked)
                    {
                        walkingIntoTreasure = true;
                        walked = true;
                    }
                    if (walkingIntoTreasure)
                    {
                        if ((distanceToMiddle*Mathf.Sign(distanceToMiddle)) > 0.2f)
                        {
                            GetComponentInParent<navMashMove>().targetPos = other.GetComponent<BoxCollider>().bounds.center;
                            GetComponentInParent<navMashMove>().Move();
                        }
                        else
                        {
                            walkingIntoTreasure = false;
                        }
                    }
                    else
                    {
                        GetComponentInParent<PlayerPieces>().treasures.RemoveAt(0);
                        turn.buttonPressed = true;
                    }
                }
            }
        }
    }
}
