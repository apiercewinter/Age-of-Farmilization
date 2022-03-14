// Boyuan Huang
// Alec Kaxon-Rupp - Debugging and Implementation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;



// WinLoseManager deals with the win scene, and button in the win scene
public class WinLoseManager : MonoBehaviour
{

    [SerializeField]
    private GameObject winCanvas;

    [SerializeField]
    private TextMeshProUGUI textField;

    private static Action disableControlDel;

    [SerializeField] private GameObject nextTurnButton;
    [SerializeField] private GameObject spawnUI1;
    [SerializeField] private GameObject spawnUI2;
    [SerializeField] private GameObject transitionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        winCanvas = this.gameObject;
        textField = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TeamManager.subscribeToWinDel(win);
        winCanvas.SetActive(false);
    }

    public void win(string playerName)
    {
        textField.text = playerName + " wins!!!";
        winCanvas.SetActive(true);
        disableControlDel();
        nextTurnButton.SetActive(false);
        spawnUI1.SetActive(false);
        spawnUI2.SetActive(false);
        transitionCanvas.SetActive(false);
      
    }

    public static void subscribeToDisableControl(Action action)
    {
        disableControlDel += action;
    }

    public void OnBackToHomeMenuButtonClick()
    {
        TeamManager.resetAll();
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
