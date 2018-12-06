using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveWall : MonoBehaviour
{

    ////rotate if true
    //public bool rotate = false;
    ////move inputblock and insert
    //public bool startMove = false;
    ////public bool moveBlocks = true;
    //public string orientation = "y";
    //public string plusOrMinus = "+";
    //public int number = 1;
    //can be rotated, moved
    //public bool finished = false;
    public AudioSource sliding;

    public void pressMove()
    {
        switch(EventSystem.current.currentSelectedGameObject.name)
        {
            case "L1":
                maze.FindObjectOfType<maze>().orientation = "x";
                maze.FindObjectOfType<maze>().plusOrMinus = "+";
                maze.FindObjectOfType<maze>().number = 1;
                break;
            case "L2":
                maze.FindObjectOfType<maze>().orientation = "x";
                maze.FindObjectOfType<maze>().plusOrMinus = "+";
                maze.FindObjectOfType<maze>().number = 2;
                break;
            case "L3":
                maze.FindObjectOfType<maze>().orientation = "x";
                maze.FindObjectOfType<maze>().plusOrMinus = "+";
                maze.FindObjectOfType<maze>().number = 3;
                break;
            case "R1":
                maze.FindObjectOfType<maze>().orientation = "x";
                maze.FindObjectOfType<maze>().plusOrMinus = "-";
                maze.FindObjectOfType<maze>().number = 1;
                break;
            case "R2":
                maze.FindObjectOfType<maze>().orientation = "x";
                maze.FindObjectOfType<maze>().plusOrMinus = "-";
                maze.FindObjectOfType<maze>().number = 2;
                break;
            case "R3":
                maze.FindObjectOfType<maze>().orientation = "x";
                maze.FindObjectOfType<maze>().plusOrMinus = "-";
                maze.FindObjectOfType<maze>().number = 3;
                break;
            case "O1":
                maze.FindObjectOfType<maze>().orientation = "y";
                maze.FindObjectOfType<maze>().plusOrMinus = "+";
                maze.FindObjectOfType<maze>().number = 1;
                break;
            case "O2":
                maze.FindObjectOfType<maze>().orientation = "y";
                maze.FindObjectOfType<maze>().plusOrMinus = "+";
                maze.FindObjectOfType<maze>().number = 2;
                break;
            case "O3":
                maze.FindObjectOfType<maze>().orientation = "y";
                maze.FindObjectOfType<maze>().plusOrMinus = "+";
                maze.FindObjectOfType<maze>().number = 3;
                break;
            case "U1":
                maze.FindObjectOfType<maze>().orientation = "y";
                maze.FindObjectOfType<maze>().plusOrMinus = "-";
                maze.FindObjectOfType<maze>().number = 1;
                break;
            case "U2":
                maze.FindObjectOfType<maze>().orientation = "y";
                maze.FindObjectOfType<maze>().plusOrMinus = "-";
                maze.FindObjectOfType<maze>().number = 2;
                break;
            case "U3":
                maze.FindObjectOfType<maze>().orientation = "y";
                maze.FindObjectOfType<maze>().plusOrMinus = "-";
                maze.FindObjectOfType<maze>().number = 3;
                break;

        }



        if (!maze.FindObjectOfType<maze>().finished)
        {
            maze.FindObjectOfType<maze>().startMove = true;
            sliding.Play();
        }
        //if(maze.FindObjectOfType<maze>().startMove == true)
        //{
        //    maze.FindObjectOfType<maze>().finished = false;
        //}
    }

    public void RotateWall()
    {
        maze.FindObjectOfType<maze>().rotate = true;
    }

    public void ActivatePattern()
    {
        maze.FindObjectOfType<maze>().patternactivated = true;
    }
    public void DeactivatePattern()
    {
        maze.FindObjectOfType<maze>().patternactivated = false;
    }
}
