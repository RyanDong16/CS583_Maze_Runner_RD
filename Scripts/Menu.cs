using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnStartButton()
    {
        Debug.Log("Start button pressed!");
        SceneManager.LoadScene("__Scene_0");

    }

    public void OnExitButton()
    {
        Debug.Log("Quit button pressed!");
        Application.Quit();
    }
}
