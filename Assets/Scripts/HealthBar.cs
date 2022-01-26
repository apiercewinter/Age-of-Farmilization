using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public int currentHealth;
    public int maxHealth;
    public Gradient gradient;
    public Image fill;

    // Set the max amount of health
    void SetMaxHealth(int amount)
    {
        maxHealth = amount;
        currentHealth = amount;
        slider.value = amount;

        fill.color = gradient.Evaluate(1f);
    }

    // substract will substract a certain amount of health from the health bar
    void substract(int amount)
    {
        slider.value -= amount;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // add will add a certain amount of health to the health bar
    void add(int amount)
    {
        slider.value += amount;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
