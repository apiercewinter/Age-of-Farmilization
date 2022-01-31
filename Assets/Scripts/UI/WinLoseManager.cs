using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{
    private static GameObject winCanvas;
    private static TextMeshProUGUI textField;

    private static Action disableControlDel;

    // Start is called before the first frame update
    void Start()
    {
        winCanvas = this.gameObject;
        textField = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        winCanvas.SetActive(false);
    }

    public static void win(string playerName)
    {
        textField.text = playerName + " wins!!!";
        winCanvas.SetActive(true);
        disableControlDel();
    }

    public static void subscribeToDisableControl(Action action)
    {
        disableControlDel += action;
    }

    public void OnBackToHomeMenuButtonClick()
    {
        Debug.Log("BackToHomeMenu");
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitButtonClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
