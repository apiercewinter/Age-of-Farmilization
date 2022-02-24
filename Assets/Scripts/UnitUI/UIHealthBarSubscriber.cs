// Boyuan Huang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UIHealthBarSubscriber deals with the healthbar at the top of the unit
public class UIHealthBarSubscriber : MonoBehaviour, ISubscriber
{
    private Slider slider;

    void Awake()
    {
        subscribe();

        slider = GetComponent<Slider>();
    }

    public void subscribe()
    {
        UIUnitCentralPublisher publisher = transform.parent.parent.GetComponent<UIUnitCentralPublisher>();
        // subscribing to the publisher
        publisher.subscribeToAddHealth(addHealth);
        publisher.subscribeToSubstractHealth(substractHealth);
        publisher.subscribeToSetMaxHealth(setMaxHealth);
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
