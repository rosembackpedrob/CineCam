using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TILT_DIRECTION
{
    UP,
    DOWN
}
public class TiltButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] CameraController cameraController;
    [SerializeField] TILT_DIRECTION TILT_direction;
    [SerializeField] [Range(-180f, 180f)] float tiltValue = 0f;
    [SerializeField] float tiltSpeed = 5f;
    [SerializeField] bool buttonOn = false;

    void Start()
    {
        //
    }
    
    void Update()
    {
        //checks every frame if it's still being pressed
        TiltUp(buttonOn, TILT_direction);
    }

    public void TiltUp(bool _buttonOn, TILT_DIRECTION _TILT_direction)
    {
        if(_buttonOn)
        {
            buttonOn = _buttonOn;
            switch(_TILT_direction)
            {
                case TILT_DIRECTION.UP:
                    cameraController.Tilt(tiltValue -= tiltSpeed * Time.deltaTime);
                    break;
                case TILT_DIRECTION.DOWN:
                    cameraController.Tilt(tiltValue += tiltSpeed * Time.deltaTime);
                    break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonOn = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonOn = false;
    }
}
