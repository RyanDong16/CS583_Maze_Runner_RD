using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Reference to the Game Over menu UI
    public GameObject gameOverMenu;

    // Reference to Victory menu UI
    public GameObject victoryMenu;

    // Subscribe to player's death & victory event
    private void OnEnable()
    {
        Player.OnPlayerDeath += EnableGameOverMenu;
        Player.OnPlayerVictory += EnableVictoryMenu;
    }

    // Unsubscribe to player's death & victory event
    private void OnDisable()
    {
        Player.OnPlayerDeath -= EnableGameOverMenu;
        Player.OnPlayerVictory -= EnableVictoryMenu;
    }

    // Enable the Game Over menu
    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    // Enable the Victory menu
    public void EnableVictoryMenu()
    {
        victoryMenu.SetActive(true);
    }

    // Restart the start menu
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}