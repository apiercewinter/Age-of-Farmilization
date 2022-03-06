//Alec Kaxon-Rupp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGameMultiplayer()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayerGameAI()
    {
        SceneManager.LoadScene("PlayerVSAI");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

}
