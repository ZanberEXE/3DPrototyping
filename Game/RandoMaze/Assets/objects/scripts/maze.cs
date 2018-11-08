using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maze : MonoBehaviour {


    //14 ecken 6 schätze
    //14 geraden
    //6 3 ecken mit schatz
    //List for found movable Walls
    private List<GameObject> list;
    //Position Array for movable Walls
    public int[,] positionArr = new int[,] {  
        { 0, 1, 0, 1, 0, 1, 0 },
        { 1, 1, 1, 1, 1, 1, 1 },
        { 0, 1, 0, 1, 0, 1, 0 },
        { 1, 1, 1, 1, 1, 1, 1 },
        { 0, 1, 0, 1, 0, 1, 0 },
        { 1, 1, 1, 1, 1, 1, 1 },
        { 0, 1, 0, 1, 0, 1, 0 } };
    //Size of Walls
    public int posSteps = 3;
    //Start Position
    public int startPosX = -9;
    public int startPosZ = 9;
    private int removePos;
    //How much to move left
    private Vector3 movement;
    private Vector2 nextPos;

    #region Testvalues
    public bool rotate = false;
    public bool startMove = false;
    public bool moveBlocks = false;
    public string orientation = "y";
    public string plusOrMinus = "-";
    public int number = 1;
    #endregion
    
    //private GameObject currentgameObject;

    private void rotateInputWall()
    {
        list.Add(GameObject.FindGameObjectWithTag("InputWall"));
        list[0].transform.rotation *= Quaternion.Euler(0, 90, 0);
        list.RemoveAt(0);
        rotate = false;
    }

    private void addWall()
    {
        list.Add(GameObject.FindGameObjectWithTag("InputWall"));
        list[0].transform.position = new Vector3(0, -3f, 0);
        Vector2 value = new Vector2(0,12);
        if (orientation=="x")
        {
            nextPos = new Vector2(-2, number);
            
        }
        else if(orientation=="y")
        {
            nextPos = new Vector2(number-2, 0);
            value = new Vector2(0, -12); 

        }
        if (plusOrMinus == "+")
        {
            list[0].transform.position = new Vector3(posSteps * 2 * nextPos.x, 0f, -posSteps * 2 * nextPos.y + value.y);
        }
        else if (plusOrMinus == "-")
        {
            list[0].transform.position = new Vector3(posSteps * 2 * -nextPos.x, 0f, -posSteps * 2 * nextPos.y + value.y);
        }
    }

    private void move(List<GameObject> walls)
    {

    }

    //find next Place for Wall
    public Vector2 firstOne(int[,] list)
    {
        
        Vector2 placment=new Vector2(0,0);
        for(int i=0; i < 7; i++)
        {
            for (int j=0; j < 7; j++)
            {
                if (list[i, j] == 1)
                {
                    list[i, j] = 2;
                    placment = new Vector2(i, j);
                    goto Return;
                }
            }
        }
        Return:
        return placment;
    }
    // Use this for initialization
    void Start () {
        
        list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Wall"));
        //list.Add(GameObject.FindGameObjectWithTag("InputWall"));
        while (list.Count > 0)
        {
            removePos = Random.Range(0, list.Count - 1);
            nextPos = firstOne(positionArr);
            //Debug.Log(nextPos);

            list[removePos].transform.position = new Vector3(nextPos.x * posSteps + startPosX, 0f, nextPos.y * -posSteps + startPosZ);
            list[removePos].transform.rotation = Quaternion.AngleAxis(Random.Range(1, 4) * 90, Vector3.up);
            list.RemoveAt(removePos);
            nextPos = Vector3.zero;

        }

    }
	
	// Update is called once per frame
	void Update () {
        if (rotate)
        {
            rotateInputWall();
        }
        if (startMove)
        {
            addWall();

            startMove = false;
        }
        if (movement.x != 0 || movement.z != 0)
        {
            move(list);
        }
	}
}
