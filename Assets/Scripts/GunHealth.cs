using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHealth : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // modify current health
    public void SetGun(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // set max health
    public void SetMaxGun(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
}
