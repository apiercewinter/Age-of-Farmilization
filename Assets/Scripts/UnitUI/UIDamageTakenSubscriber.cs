// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// UIDamageTakenSubscriber deals with the damage a certain unit takes
public class UIDamageTakenSubscriber : MonoBehaviour, ISubscriber
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
        // In order to keep the TakenDamageIndicator in the end of the HealthBar, I put the Indicator under 
        // HealthBar as a child of HealthBar, so, here it needs an extra parent to get to the UIUnitCentralPublisher
        UIUnitCentralPublisher publisher = transform.parent.parent.parent.GetComponent<UIUnitCentralPublisher>();
        // subscribing to the publisher
        publisher.subscribeToSubstractHealth(setDamageTaken);
        publisher.subscribeToAddHealth(setDamageAdded);
    }

    // Set how many damage does the character takes and transform it into a text format
    // and call FadeTextToZeroAplha() to start the fade away "animation"
    public void setDamageTaken(float amount)
    {
        textField.text = "-" + amount;
        // Set the color to the not transparent one before starting animation
        textField.color = fullAlphaColor;
        StartCoroutine(fadeTextToZeroAlpha(2));

    }

    public void setDamageAdded(float amount)
    {
        textField.text = "+" + amount;
        // Set the color to the not transparent one before starting animation
        textField.color = fullAlphaColor;
        StartCoroutine(fadeTextToZeroAlpha(2));
    }

    // This method will make the text to be transparent over "time" seconds, this is used to create
    // a fade away effect for the text. Copied from Unity forum question:
    // https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
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
