 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandoMazeBoard : MonoBehaviour {

    //List of Players
    public List<TurnClass> playersGroup;
    public List<GameObject> playersNum;

    //public PlayerPieces playerPieces = new PlayerPieces();
    //public List<PlayerPieces> playerPieces;

    //gameObjects of Players
    public GameObject playerPiecePrefab;

    //temp PlayerNum Solution
    const int playerIndex = 2;
    const int removeNum = 0;

    public int playerNum = 4;

	// Use this for initialization
	void Start ()
    {
        //temp Solution changing number of players
        playersGroup.RemoveRange(playerIndex, removeNum);

        //GenerateBoard();

        //Reset turns in the beginning
        ResetTurns();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTurns();
	}

    //reset turn to Beginning
        //Player1 starts
        //rest didn't have their turn yet in this round
    private void ResetTurns()
    {
        for(int i = 0; i < playersGroup.Count; i++)     //go through each player one by one
        {
            if(i == 0)  //start with first player
            {
                playersGroup[i].isTurn = true;
                playersGroup[i].wasTurnPrev = false;
            }
            else        //rest set both false
            {
                playersGroup[i].isTurn = false;
                playersGroup[i].wasTurnPrev = false;
            }
        }
    }

    //will be called in Update()
    //Update Player Turns
        //set current Player to isTurn = true
        //set previous Player 
    private void UpdateTurns()
    {
        for(int i = 0; i < playersGroup.Count; i++) //go through each player
        {
            //if Player didn't have his turn yet
                //set isTurn to true
                //break the loop so it doesn't get resetted
            if (!playersGroup[i].wasTurnPrev)   
            {
                playersGroup[i].isTurn = true;
                break;
            }
                //if iteration = amount of players And the last Player had his turn
                    //reset the turns -> new round
            else if (i == playersGroup.Count - 1 && playersGroup[i].wasTurnPrev)
                ResetTurns();
        }
    }

    //create pieces on board
    //private void GenerateBoard()
    //{
    //    for(int i = 0; i < playerNum; i++)
    //    {
    //        GeneratePlayerPiece(i, 0);
    //    }
    //}

    private void GeneratePlayerPiece(int x, int y)
    {
        GameObject go = Instantiate(playerPiecePrefab) as GameObject;
        go.transform.SetParent(transform);
        PlayerPieces pp = go.GetComponent<PlayerPieces>();
        playersNum.Add(go);
        pp.transform.position = (Vector3.right * x)+ (Vector3.forward * y)+ (Vector3.up * 1.5f);      
    }
}

    //needed for List
[System.Serializable]
public class TurnClass
{
    public GameObject playerGameObject;
    public bool isTurn = false;
    public bool wasTurnPrev = false;
}
