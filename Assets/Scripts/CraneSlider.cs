using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraneSlider : MonoBehaviour
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
        //internal variable
        sliderValue = _sliderValue;

        cameraController.Crane(_sliderValue);
    }
}
