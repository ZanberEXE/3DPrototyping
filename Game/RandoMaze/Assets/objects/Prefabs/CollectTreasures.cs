using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (walkingIntoTreasure)
        {
            distanceToMiddle = Vector2.Distance(new Vector2(this.GetComponentInParent<NavMeshAgent>().destination.x, this.GetComponentInParent<NavMeshAgent>().destination.z), new Vector2(transform.position.x, transform.position.z));
            if ((distanceToMiddle * Mathf.Sign(distanceToMiddle)) > 0.2f)
            {
                GetComponentInParent<navMashMove>().targetPos = this.GetComponentInParent<NavMeshAgent>().destination;
                GetComponentInParent<navMashMove>().Move();
            }
            else
            {
                walkingIntoTreasure = false;

            }
        }
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
                        GetComponentInParent<PlayerPieces>().treasures[0].GetComponentInParent<TreasureCard>().TreasureCardObj.SetActive(false);
                        GetComponentInParent<PlayerPieces>().treasures[0].SetActive(false);
                        GetComponentInParent<PlayerPieces>().treasures.RemoveAt(0);
                        turn.buttonPressed = true;
                        walked = false;
                    }
                }
            }
        }
    }
}
