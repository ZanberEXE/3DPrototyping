using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class maze : MonoBehaviour {


    //14 ecken 6 schätze
    //14 geraden
    //6 3 ecken mit schatz
    //List for found movable Walls
    public List<GameObject> list;
    //public List<Vector3> targetWalls;
    
    public List<NavMeshSurface> surfaces;
    public Vector3 moveto;
    GameObject usable;
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
    private int posSteps = 3;
    //Start Position
    private int startPosX = -9;
    private int startPosZ = 9;
    private int removePos;
    //How much to move left
    private Vector3 movement;
    private Vector2 nextPos;
    private int moveValue;

    #region Testvalues
    //can be rotated, moved
    public bool finished = false;
    //rotate if true
    public bool rotate = false;
    //move inputblock and insert
    public bool startMove = false;
    //public bool moveBlocks = true;
    public string orientation = "y";
    public string plusOrMinus = "+";
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

    //Position InputWall for pushing the Row/Col
    public void addWall()
    {
        list.Add(GameObject.FindGameObjectWithTag("InputWall"));
        list[0].transform.position = new Vector3(0, -3f, 0);
        Vector2 value = new Vector2(1,12);
        if (orientation=="x")
        {
            nextPos = new Vector2(-2, number);
            
        }
        else if(orientation=="y")
        {
            nextPos = new Vector2(number-2, 0);
            value = new Vector2(-1, 12);

        }
        if (plusOrMinus == "+")
        {
            list[0].transform.position = new Vector3(posSteps * 2 * nextPos.x, 0f, -posSteps * 2 * nextPos.y + value.y);
        }
        else if (plusOrMinus == "-")
        {
            list[0].transform.position = new Vector3(-posSteps * 2 * nextPos.x * value.x, 0f, -posSteps * 2 * nextPos.y + value.y * value.x);
        }
    }

    //Push Row/Col and make new InputWall
    public void moveTo()
    {
        int zRows = 0;
        switch (number)
        {
            case 1:
                zRows = 1*2*posSteps;
                break;
            case 2:
                zRows = 0;
                break;
            case 3:
                zRows = -1*2*posSteps;
                break;
            default:
                break;
        }
        if (orientation == "x")
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Wall"))
            {
                if (gameObject.transform.position.z == zRows)
                {
                    list.Add(gameObject);
                    //targetWalls.Add(new Vector3());
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (plusOrMinus == "+")
                {
                    moveValue = 1;
                }
                else if(plusOrMinus=="-")
                {
                    moveValue = -1;
                }
                moveto = list[i].transform.position + posSteps * new Vector3(moveValue, 0, 0);
                list[i].transform.position = moveto;
            }
            list.Find(x => x.tag.Equals("InputWall")).tag = "Wall";
            if (plusOrMinus == "+")
            {
                list.Find(x => x.transform.position.x > 9).tag = "InputWall";
            }else if (plusOrMinus == "-")
            {
                list.Find(x => x.transform.position.x < -9).tag = "InputWall";
            }
        }
        else if(orientation=="y")
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Wall"))
            {
                if (gameObject.transform.position.x == zRows*-1)
                {
                    list.Add(gameObject);
                    //targetWalls.Add(new Vector3());
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (plusOrMinus == "+")
                {
                    moveValue = -1;
                }
                else if (plusOrMinus == "-")
                {
                    moveValue = 1;
                }
                moveto = list[i].transform.position + posSteps * new Vector3(0, 0, moveValue);
                list[i].transform.position = moveto;
            }
            list.Find(x => x.tag.Equals("InputWall")).tag = "Wall";
            if (plusOrMinus == "+")
            {
                list.Find(x => x.transform.position.z < -9).tag = "InputWall";
            }
            else if (plusOrMinus == "-")
            {
                list.Find(x => x.transform.position.z > 9).tag = "InputWall";
            }
        }
        //empty list
        list.Clear();
        //add disable Button
    }

    private void move(List<GameObject> walls)
    {
        //for continous movement
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
        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!finished)
        {
            if (rotate)
            {
                rotateInputWall();
            }
            if (startMove)
            {
                addWall();
                moveTo();
                for (int i = 0; i < surfaces.Count; i++)
                {
                    surfaces[i].BuildNavMesh();
                }
                finished = true;
                startMove = false;
            }
        }
        if (movement.x != 0 || movement.z != 0)
        {
            move(list);
        }
	}
}
