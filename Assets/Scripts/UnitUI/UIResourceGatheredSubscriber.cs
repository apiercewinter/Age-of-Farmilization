// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// UIResourceGatheredSubscriber will indicate player that the player just gathered resource
public class UIResourceGatheredSubscriber : MonoBehaviour, ISubscriber
{
    private TextMeshProUGUI textField;
    private Color fullAlphaColor;

    // Start is called before the first frame update
    void Start()
    {
        subscribe();

        textField = GetComponent<TextMeshProUGUI>();
        textField.text = "";
        fullAlphaColor = textField.color;
    }

    public void subscribe()
    {
        UIUnitCentralPublisher publisher = transform.parent.parent.parent.GetComponent<UIUnitCentralPublisher>();
        // subscribing to the publisher
        publisher.subscribeToGatheringResource(indicateGatheredResource);
    }

    public void indicateGatheredResource(string resource, int amount)
    {
        textField.text = "Collected " + amount + " " + resource;
        // Set the color to the not transparent one before starting animation
        textField.color = fullAlphaColor;
        StartCoroutine(fadeTextToZeroAlpha(2));
    }

    public IEnumerator fadeTextToZeroAlpha(float time)
    {
        textField.color = new Color(textField.color.r, textField.color.g, textField.color.b, 1);
        while (textField.color.a > 0.0f)
        {
            // Create a fade away effect by decreasing the alpha value of the color of the text
            // The lower the alpha value is, the more tranparent the text is, vice versa.
            textField.color = new Color(textField.color.r, textField.color.g, textField.color.b, textField.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }
}
