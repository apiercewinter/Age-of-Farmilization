using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public float currentHealth;
    public float maxHealth;
    public Gradient gradient;
    public Image fill;

    void Start()
    {
        SetMaxHealth(50);
    }


    // Set the max amount of health
    void SetMaxHealth(float amount)
    {
        maxHealth = amount;
        currentHealth = amount;
        slider.maxValue = amount;
        slider.value = amount;

        fill.color = gradient.Evaluate(1f);
    }

    // substract will substract a certain amount of health from the health bar
    void substract(float amount)
    {
        slider.value -= amount;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // add will add a certain amount of health to the health bar
    void add(float amount)
    {
        slider.value += amount;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // This method will subscribe to the Health class, which will notify when taking damage
}
