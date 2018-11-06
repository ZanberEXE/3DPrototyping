using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maze : MonoBehaviour {
    public int num_rows=2;
    public int num_cols=2;
    Random random;

    public GameObject[] row0 = new GameObject[4];
    public GameObject[] row1 = new GameObject[4];
    public GameObject[] row2 = new GameObject[4];
    public GameObject[] row3 = new GameObject[4];
    public List<GameObject> list;
    //public GameObject[] row4 = new GameObject[9];
    //public GameObject[] row5 = new GameObject[9];
    //public GameObject[] row6 = new GameObject[9];
    //public GameObject[] row7 = new GameObject[9];
    //public GameObject[] row8 = new GameObject[9];
    // Use this for initialization
    void Start () {
        random = new Random();
        list = new List<GameObject>(GameObject.FindGameObjectsWithTag("Wall"));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
