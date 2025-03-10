using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ZoomSlider : MonoBehaviour
{
    [SerializeField]CameraController cameraController;
    [SerializeField] float sliderValue;

    void Start()
    {
        sliderValue = this.gameObject.GetComponent<Slider>().value;
    }

    void Update()
    {
        //
    }

    public void ChangeSliderValue(float _sliderValue)
    {
        sliderValue = _sliderValue;
        cameraController.ChangeLength(sliderValue);
    }

}
