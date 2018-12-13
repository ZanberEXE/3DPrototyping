using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class maze : MonoBehaviour
{


    //14 ecken 6 schätze
    //14 geraden
    //6 3 ecken mit schatz
    //List for found movable Walls
    public List<GameObject> list;
    private string winner = " hat gewonnen";
    public GameObject winnscreen;
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
    //rot speed of walls
    public float rotSpeed = 5f;
    public float rotLeft;
    public float rotFor;
    public bool rotating = false;
    public bool buttonsDisabled = false;
    //speed of walls
    private float speed = 0.5f;
    private Vector3 moveleft;
    private float range;
    //for treasures
    private GameObject mazeSystem;
    public List<Vector3> playerTreasurePos;
    //SFX
    public AudioSource rotationpattern;
    private bool play = false;

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
    public bool moving = false;
    //last button
    public List<GameObject> buttons;
    public GameObject button;
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
                    list.Add(gameObject);
                    
                }
            }
            rotLeft = 90;
            rotating = true;
        }
        else if (porientation == "y")
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Wall"))
            {
                if (gameObject.transform.position.x == zRows * -1)
                {
                    list.Add(gameObject);
                }
            }
            rotLeft = 90;
            rotating = true;
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

    private void rotatePatternAnim()
    {
        rotFor = rotSpeed * Time.deltaTime;
        if (rotLeft > rotFor)
        {
            rotLeft -= rotFor;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].transform.Rotate(0, rotFor, 0);
            }
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].transform.rotation = Quaternion.Euler(0, Mathf.Round(list[i].transform.rotation.eulerAngles.y / 90) * 90, 0);
            }
            list.Clear();
            rotating = false;
        }
    }
    private void rotateAnim(GameObject gameObject)
    {
        rotFor = rotSpeed * Time.deltaTime;
        if (rotLeft > rotFor)
        {
            rotLeft -= rotFor;
            gameObject.transform.Rotate(0,rotFor,0);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Round(gameObject.transform.rotation.eulerAngles.y/90)*90, 0);
            list.RemoveAt(0);
            rotating = false;
        }
    }
    //rotate the free wall
    private void rotateInputWall()
    {
        if (!rotating)
        {
            list.Add(GameObject.FindGameObjectWithTag("InputWall"));
            rotLeft = 90;
            rotating = true;
            rotate = false;
        }
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

    //saves last moved row/col
    void lastbutton()
    {
        if (orientation == "x")
        {
            if (plusOrMinus=="+")
            {
                if (button != buttons.Where(obj => obj.name == Convert.ToString("R" + number)).Single())
                {
                    button = buttons.Where(obj => obj.name == Convert.ToString("R" + number)).Single();
                }
            }
            else if(plusOrMinus=="-")
            {
                if (button != buttons.Where(obj => obj.name == Convert.ToString("L" + number)).Single())
                {
                    button = buttons.Where(obj => obj.name == Convert.ToString("L" + number)).Single();
                }
            }
        }else if (orientation == "y")
        {
            if (plusOrMinus == "+")
            {
                if (button != buttons.Where(obj => obj.name == Convert.ToString("U" + number)).Single())
                {
                    button = buttons.Where(obj => obj.name == Convert.ToString("U" + number)).Single();
                }
            }
            else if (plusOrMinus == "-")
            {
                if (button != buttons.Where(obj => obj.name == Convert.ToString("O" + number)).Single())
                {
                    button = buttons.Where(obj => obj.name == Convert.ToString("O" + number)).Single();
                }
            }
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
        moving = true;
        lastbutton();
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
        moving = false;
        GameObject.FindGameObjectWithTag("InputWall").GetComponent<Transform>().transform.position = new Vector3(18, 0, -3);
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
        buttons = new List<GameObject>(GameObject.FindGameObjectsWithTag("button"));
        list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Wall"));
        GameObject.FindGameObjectWithTag("InputWall").GetComponent<Transform>().transform.position = new Vector3(18, 0, -3);

        //list.Add(GameObject.FindGameObjectWithTag("InputWall"));
        while (list.Count > 0)
        {
            removePos = UnityEngine.Random.Range(0, list.Count - 1);
            nextPos = firstOne(positionArr);
            //Debug.Log(nextPos);

            list[removePos].transform.position = new Vector3(nextPos.x * posSteps + startPosX, 0f, nextPos.y * -posSteps + startPosZ);
            list[removePos].transform.rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(1, 4) * 90, Vector3.up);
            list.RemoveAt(removePos);
            nextPos = Vector3.zero;
            
        }
        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
        mazeSystem = GameObject.Find("Maze");
        for (int i = 0; i < 4; i++)
        {
            GetComponentInParent<RandoMazeBoard>().playersGroup[i].playerGameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < GetComponentInParent<RandoMazeBoard>().playerNum; i++)
        {
            if (GetComponentInParent<RandoMazeBoard>().playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().reachedGoal)
            {
                foreach (GameObject UIElement in GameObject.FindGameObjectsWithTag("UI"))
                {
                    UIElement.SetActive(false);
                }
                winnscreen.transform.Find("Panel").transform.Find("Text").GetComponent<Text>().text = Convert.ToString(GetComponentInParent<RandoMazeBoard>().playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().name+winner);
                winnscreen.SetActive(true);
            }
        }
        for (int i = 0; i < mazeSystem.GetComponent<RandoMazeBoard>().playerNum; i++)
        {
            if (mazeSystem.GetComponent<RandoMazeBoard>().playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().treasures.Count != 0)
            {
                int intern = i + 1;
                if (mazeSystem.GetComponent<RandoMazeBoard>().playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().treasures[0] != mazeSystem.GetComponent<RandoMazeBoard>().playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().goal)
                {
                   mazeSystem.GetComponent<RandoMazeBoard>().UiPlayer.transform.Find(Convert.ToString("Spieler " + intern + " Bild")).transform.Find(Convert.ToString("Spieler " + intern)).GetComponent<Text>().text = Convert.ToString(mazeSystem.GetComponent<RandoMazeBoard>().treasuresForPlayer - mazeSystem.GetComponent<RandoMazeBoard>().playersGroup[i].playerGameObject.GetComponent<PlayerPieces>().treasures.Count + "/" + mazeSystem.GetComponent<RandoMazeBoard>().treasuresForPlayer);
                }
                else
                {
                    mazeSystem.GetComponent<RandoMazeBoard>().UiPlayer.transform.Find(Convert.ToString("Spieler " + intern + " Bild")).transform.Find(Convert.ToString("Spieler " + intern)).GetComponent<Text>().text = "6/6";
                }
            }
        }
        if (rotating&&rotLeft!=0)
        {
            buttonsDisabled = true;
            if (list.Count == 1)
            {
                rotateAnim(list[0]);
            }
            else
            {
                rotatePatternAnim();
            }
        }
        else
        {
            
            rotating = false;
        }
        if (moving)
        {
            buttonsDisabled = true;
        }
        else if(!rotating&&!finished)
        {
            buttonsDisabled = false;
        }
        if (buttonsDisabled)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }
        }else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i] != button)
                {
                    buttons[i].SetActive(true);
                }
            }
        }
        if (pattern&&patternactivated)
        {
            if (play == true)
            {
                PlaySFX();
            }
            else
            {
                play = true;
            }
            rotatePattern();
            pattern = false;

        }
        if (!finished)
        {
            
            if (rotate)
            {
                rotateInputWall();
                PlaySFX();
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

    private void PlaySFX()
    {
        rotationpattern.Play();
    }
}
