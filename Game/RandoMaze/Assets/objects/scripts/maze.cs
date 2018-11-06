using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maze : MonoBehaviour {


    //14 ecken 6 schätze
    //14 geraden
    //6 3 ecken mit schatz
    public List<GameObject> list;
    public int[,] positionArr;
    public int posSteps = 3;
    public int startPosX = -6;
    public int startPosZ = 12;
    private int removePos;

    Vector2 nextPos;

    private GameObject currentgameObject;
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
        positionArr = new int[,] {  { 0, 1, 0, 1, 0, 1, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 1, 0, 1, 0, 1, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 1, 0, 1, 0, 1, 0 },
                                    { 1, 1, 1, 1, 1, 1, 1 },
                                    { 0, 1, 0, 1, 0, 1, 0 } };
        list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Wall"));
        //list.Add(GameObject.FindGameObjectWithTag("InputWall"));
        while (list.Count > 0)
        {
            removePos = Random.Range(0, list.Count - 1);
            nextPos = firstOne(positionArr);
            Debug.Log(nextPos);

            list[removePos].transform.position = new Vector3(nextPos.x * posSteps + startPosX, 0f, nextPos.y * -posSteps + startPosZ);
            list[removePos].transform.rotation = Quaternion.AngleAxis(Random.Range(1, 4) * 90, Vector3.up);
            list.RemoveAt(removePos);

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
