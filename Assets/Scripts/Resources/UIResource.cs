using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResource : MonoBehaviour
{
    [SerializeField]
    private GameObject Highlight;
    [SerializeField]
    private GameObject Display;
    [SerializeField]
    private GameObject Text;

    private ResourceObject resourceObj;

    // Start is called before the first frame update
    void Start()
    {
        resourceObj = gameObject.GetComponent<ResourceObject>();
    }

    private void OnMouseOver()
    {
        int resourceSupply = resourceObj.getResourceSupply();
        string name = resourceObj.getResourcename();
        Text.GetComponent<TextMeshProUGUI>().text = "You are looking at " + name + ", it has " + resourceSupply + " left.";
        Highlight.SetActive(true);
        Display.SetActive(true);
    }

    private void OnMouseExit()
    {
        Highlight.SetActive(false);
        Display.SetActive(false);
    }
}
