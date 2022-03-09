//Alec Kaxon-Rupp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Starts Multiplayer Match
    public void PlayGameMultiplayer()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Starts AI Match
    public void PlayerGameAI()
    {
        SceneManager.LoadScene("PlayerVSAI");
    }

    //Quits the game
    public void QuitGame ()
    {
        Application.Quit();
    }

    //Returns to the MainMenu
    public void ReturnMainMenu()
    {
        TeamManager.resetAll();
        SceneManager.LoadScene("Menu");
    }

}
