using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class maze : MonoBehaviour
{


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
    public int[,] positionArr = new int[,] 
    {  
        { 0, 1, 0, 1, 0, 1, 0 },
        { 1, 1, 1, 1, 1, 1, 1 },
        { 0, 1, 0, 1, 0, 1, 0 },
        { 1, 1, 1, 1, 1, 1, 1 },
        { 0, 1, 0, 1, 0, 1, 0 },
        { 1, 1, 1, 1, 1, 1, 1 },
        { 0, 1, 0, 1, 0, 1, 0 }
    };
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
    //speed of walls
    private float speed=0.5f;
    private Vector3 moveleft;
    private float range;

    #region for options
    //is the pattern activated
    public bool patternactivated = true;
    public bool rotate = false;
    #endregion

    #region Testvalues
    //pattern parameters
    public bool pattern = false;
    public string porientation = "y";
    public int pnumber = 1;
    private bool won = false;
    //can be rotated, moved
    public bool finished = false;
    //rotate if true
    
    //move inputblock and insert
    public bool startMove = false;
    //input parameters
    public string orientation = "y";
    public string plusOrMinus = "+";
    public int number = 1;
    #endregion
    
    //rotation of the rotation pattern
    private void rotatePattern()
    {
        int zRows = 0;
        switch (pnumber)
        {
            case 1:
                zRows = 1 * 2 * posSteps;
                break;
            case 2:
                zRows = 0;
                break;
            case 3:
                zRows = -1 * 2 * posSteps;
                break;
            default:
                break;
        }
        if (porientation == "x")
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Wall"))
            {
                if (gameObject.transform.position.z == zRows)
                {
                    gameObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
                }
            }
        }
        else if (porientation == "y")
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Wall"))
            {
                if (gameObject.transform.position.x == zRows * -1)
                {
                    gameObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
                }
            }
        }
        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
        changePattern();
    }

    //nect row/col for pattern
    private void changePattern()
    {
        switch (pnumber)
        {
            case 3:
                if (porientation == "y")
                {
                    porientation = "x";
                }
                else if (porientation=="x")
                {
                    porientation = "y";
                }
                pnumber = 1;
                break;
            default:
                pnumber++;
                break;
        }
    }

    //rotate the free wall
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
                movement = posSteps * new Vector3(moveValue, 0, 0);
                moveleft = movement;
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
                movement = posSteps * new Vector3(0, 0, moveValue);
                moveleft = movement;
            }
        }
        
        //add disable Button
    }

    //continuous movement
    private void move(List<GameObject> walls)
    {
        
        range = Vector3.Distance(new Vector3(0, 0, 0), moveleft -= movement * speed * Time.deltaTime);
        moveto = movement * speed * Time.deltaTime;

        if (range>Vector3.Distance(new Vector3(0,0,0),moveto))
        {
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].transform.position += moveto;
            }
        }
        else
        {
            for(int i = 0; i < walls.Count; i++)
            {
                walls[i].transform.position += (moveleft -= movement * speed * Time.deltaTime);
                moveleft = Vector3.zero;
            }
            finishMove(walls);
        }
        //for continous movement
    }

    //end of movement
    private void finishMove(List<GameObject> walls)
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].transform.position = new Vector3(Mathf.Round(walls[i].transform.position.x), 0, (Mathf.Round(walls[i].transform.position.z)));
        }
        list.Find(x => x.tag.Equals("InputWall")).tag = "Wall";
        if (orientation == "x")
        {
            if (plusOrMinus == "+")
            {
                list.Find(x => x.transform.position.x > 9).tag = "InputWall";
            }
            else if (plusOrMinus == "-")
            {
                list.Find(x => x.transform.position.x < -9).tag = "InputWall";
            }
        }
        else if (orientation == "y")
        {
            if (plusOrMinus == "+")
            {
                list.Find(x => x.transform.position.z < -9).tag = "InputWall";
            }
            else if (plusOrMinus == "-")
            {
                list.Find(x => x.transform.position.z > 9).tag = "InputWall";
            }
        }
        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
        list.Clear();

        GameObject.FindGameObjectWithTag("InputWall").GetComponent<Transform>().transform.position = new Vector3(20, 0, 5);
        GameObject.Find("UI").GetComponent<EndTurn>().buttonPressed = false;
        finished = true;
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
        GameObject.FindGameObjectWithTag("InputWall").GetComponent<Transform>().transform.position = new Vector3(20, 0, 5);

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
        for (int i = 0; i < GetComponentInParent<RandoMazeBoard>().playerNum; i++)
        {
            if (GetComponentInParent<RandoMazeBoard>().playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().reachedGoal)
            {
                SceneManager.LoadScene(4);
            }
        }
        
        if (pattern&&patternactivated)
        {
            rotatePattern();
            pattern = false;
        }
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
                startMove = false;
            }
        }
        if (moveleft.x != 0 || moveleft.z != 0)
        {
            move(list);
            
        }
	}
}
