using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject transitionCanvas;
    private TextMeshProUGUI textField;

    void Start()
    {
        Debug.Log("this is called");
        // Child "Message" in the TransitionCanvas
        Debug.Log("canvas name: " + transitionCanvas.name);
        Debug.Log("third child name: " + transitionCanvas.transform.GetChild(2).gameObject.name);
        transitionCanvas.SetActive(false);
        textField = transitionCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        textField.text = "";
    }

    public void showTransitionCanvas(string nextPlayerName)
    {
        textField.text = "Now it is " + nextPlayerName + "'s turn, get ready!";
        transitionCanvas.SetActive(true);
    }

    public void onImReadyButtonClick()
    {
        transitionCanvas.SetActive(false);
    }
}
