using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void SetupMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetupValue(float value)
    {
        slider.value = value;
    }
}
