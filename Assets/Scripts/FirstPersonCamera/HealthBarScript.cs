using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private HealthPlayer healthPlayer;

    private void Awake()
    {
        healthPlayer = GameObject.FindObjectOfType<HealthPlayer>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void getHealthBar()
    {
        slider.value = healthPlayer.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
}
