using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderNumber : MonoBehaviour
{
    [SerializeField]
    private Text sliderNumber;

    private Slider slider;

    void Start()
    {
        slider = this.GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChange);
    }

    public void OnValueChange(float value)
    {
        float multi = slider.wholeNumbers == false ? 10 : 1;
        sliderNumber.text = ((int)(value * multi)).ToString();
    }
}
