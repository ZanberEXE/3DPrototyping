using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPieces: MonoBehaviour
{

    public RandoMazeBoard boardTurnSystem;
    public TurnClass turnClass;
    public EndTurn endPlayerTurn;

    //wallmovement system
    public maze maze;
    public bool finishedwall = false;

    //treasure
    public int playernmb = 0;
    public List<GameObject> treasures;
    public GameObject goal;
    public bool hasTreasures = false;

    keyMove2 move;
    navMashMove NVM;
    public bool isTurn = false;     //bool to check if itz the Player's turn

    private void checkGoal()
    {
        if (treasures.Count == 0)
        {
            treasures.Add(goal);
        }
    }
    private void Start()
    {
        boardTurnSystem = GameObject.Find("Maze").GetComponent<RandoMazeBoard>();
        maze = GameObject.Find("Maze").GetComponent<maze>();
        move = GetComponent<keyMove2>();
        NVM = GetComponent<navMashMove>();

        //for each gameObject of Turnclass in the List of "Maze"
        foreach (TurnClass element in boardTurnSystem.playersGroup)
        {
            //if the gameObject Name matches the gameObject name in "Maze" /check if gameObject is registered in "Maze"
            //add reference of element instance into turnclass
            if (element.playerGameObject.name == gameObject.name)
                turnClass = element;
        }

    }

    private void Update()
    {
        //set value of isTurn equal to value that current Player holds
        isTurn = turnClass.isTurn;
        finishedwall = maze.finished;

        if (maze.GetComponent<RandoMazeBoard>().playerNum != playernmb)
        {
            playernmb = maze.GetComponent<RandoMazeBoard>().playerNum;
        }
        if (hasTreasures)
        {
            checkGoal();
        }
        //if walls moved
        if (finishedwall)
        {
            //if isTurn = true
            if(isTurn)
            {
                move.moving = true;
                NVM.moving2 = true;

                //check if Button was pressed
                if (endPlayerTurn.buttonPressed == true)
                {
                    isTurn = false;     //set isTurn false again
                    turnClass.isTurn = isTurn;      //turnClass.isTurn = false
                    turnClass.wasTurnPrev = true;   //set the Players wasTurnPrev to true

                    endPlayerTurn.buttonPressed = false;    //change EndTurn Button was pressed to false again

                    move.moving = false;
                    NVM.moving2 = false;
                    finishedwall = false;
                    maze.finished = false;
                }
            }
        }
        
    }
}
