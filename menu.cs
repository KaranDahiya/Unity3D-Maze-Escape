using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // loads scene with game instructions
    public void loadControlsScene()
    {
        SceneManager.LoadScene("Controls");
    }

    // starts game
    public void loadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    // returns to menu
    public void loadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // exits application
    public void exitGame()
    {
        Application.Quit();
    }
}
