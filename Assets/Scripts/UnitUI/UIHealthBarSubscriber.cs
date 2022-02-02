using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Writer: Boyuan Huang

// UIHealthBarSubscriber deals with the healthbar at the top of the unit
public class UIHealthBarSubscriber : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        UIUnitCentralPublisher publisher = transform.parent.parent.GetComponent<UIUnitCentralPublisher>();
        // subscribing to the publisher
        publisher.subscribeToAddHealth(addHealth);
        publisher.subscribeToSubstractHealth(substractHealth);
        publisher.subscribeToSetMaxHealth(setMaxHealth);

        slider = GetComponent<Slider>();
    }

    public void addHealth(float amount)
    {
        slider.value += amount;
    }

    public void substractHealth(float amount)
    {
        slider.value -= amount;
    }

    public void setMaxHealth(float amount)
    {
        slider.maxValue = amount;
        slider.value = amount;
    }
}
