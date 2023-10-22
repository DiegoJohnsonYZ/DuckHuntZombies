using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderNumber : MonoBehaviour
{
    [SerializeField]
    private Text sliderNumber;

    void Start()
    {
        this.GetComponent<Slider>().onValueChanged.AddListener(OnValueChange);
    }

    public void OnValueChange(float value)
    {
        sliderNumber.text = ((int)value).ToString();
    }
}
