using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour {

    public bool buttonPressed = false;

    public void EndPlayerTurn()
    {
        //Debug.Log("Button was pressed!");
        buttonPressed = true;
    }

    public void Rotate()
    {
        maze.FindObjectOfType<maze>().rotate = true;
    }
}
