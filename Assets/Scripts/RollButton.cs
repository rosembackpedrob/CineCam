using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ROLL_DIRECTION
{
    LEFT,
    RIGHT
}
public class RollButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] CameraController cameraController;
    [SerializeField] ROLL_DIRECTION direction;
    [SerializeField] [Range(-90f, 90f)] float rollValue = 0f;
    [SerializeField] float tiltSpeed = 5f;
    [SerializeField] bool buttonOn = false;

    void Start()
    {
        //
    }
    
    void Update()
    {
        //checks every frame if it's still being pressed
        RollUp(buttonOn, direction);
    }

    public void RollUp(bool _buttonOn, ROLL_DIRECTION _direction)
    {
        if(_buttonOn)
        {
            buttonOn = _buttonOn;
            switch(_direction)
            {
                case ROLL_DIRECTION.LEFT:
                    cameraController.Roll(rollValue -= tiltSpeed * Time.deltaTime);
                    break;
                case ROLL_DIRECTION.RIGHT:
                    cameraController.Roll(rollValue += tiltSpeed * Time.deltaTime);
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
