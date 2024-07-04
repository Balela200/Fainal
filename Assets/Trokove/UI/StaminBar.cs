using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMana(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }
    public void SetMana(int stamina)
    {
        slider.value = stamina;
    }
}
