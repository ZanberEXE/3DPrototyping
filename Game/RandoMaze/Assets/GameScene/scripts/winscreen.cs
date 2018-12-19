using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winscreen : MonoBehaviour
{

    public AudioSource drumRoll;
    public AudioSource fanfarre;
	// Use this for initialization
	void Start ()
    {
        drumRoll.Play();
        fanfarre.PlayDelayed(4);
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
    public void returnToMainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
