using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

//Alec Kaxon-Rupp


public class WinLoseTemp : MonoBehaviour
{

    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    [SerializeField]
    private GameObject winCanvas;

    [SerializeField]
    private TextMeshProUGUI textField;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player1.transform.childCount<1)
        {
            win("Player 2");
        }

        if (player2.transform.childCount < 1)
        {
            win("Player 1");
        }
    }

    public void win(string playerName)
    {
        textField.text = playerName + " wins!!!";
        winCanvas.SetActive(true);
        //disableControlDel();
    }

    public void OnBackToHomeMenuButtonClick()
    {
        TeamManager.resetAll();
        Debug.Log("BackToHomeMenu");
        SceneManager.LoadScene("Menu");

    }

    public void OnQuitButtonClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


}
