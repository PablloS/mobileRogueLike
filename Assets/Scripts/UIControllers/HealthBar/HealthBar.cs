using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBar : MonoBehaviour
{
    public Slider slider; 

    public void SetMaxHeath(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHeath(float health)
    {
        slider.value = health; 
    }
}
