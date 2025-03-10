using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayPathButton : MonoBehaviour
{
    [SerializeField] CameraController cameraController;

    void Start()
    {
        //
    }
    
    void Update()
    {
        //
    }

    public void PlayPath()
    {
        cameraController.StartPath();
    }
}
