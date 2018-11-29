using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public AudioSource click;
    public void Exit()
    {
        click.Play();
        SceneManager.LoadScene(0);
    }
}
