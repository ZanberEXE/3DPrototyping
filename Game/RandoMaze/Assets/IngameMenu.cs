using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour {

    public GameObject helpButton;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void HelpButton()
    {
        if (helpButton.activeInHierarchy == true)
        {
            helpButton.SetActive(false);
        }
        else
        {
            helpButton.SetActive(true);
        }
    }
}
