using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    [SerializeField] Camera sceneCamera;

    const float rotRange = 180f;
    [Header("Rotation Values")]
    [SerializeField] [Range(-rotRange, rotRange)] float panValue;
    [SerializeField] [Range(-rotRange, rotRange)] float tiltValue;
    [SerializeField] [Range(-rotRange, rotRange)] float rollValue;
    [SerializeField] float rotSpeed = 0.8f;

    [Header("Crane")]
    [SerializeField] float cameraHeight;

    [Header("Camera Transforms")]
    [SerializeField] Vector3 cameraRotation;
    [SerializeField] Vector3 cameraPosition;
 
    const float flMinRange = 5f;
    const float flMaxRange = 70f;
    [Header("Focal Length (in mm)")]
    [SerializeField] [Range(-flMinRange, flMaxRange)] float cameraFL;

    [Header("Rail (Path System)")]
    [SerializeField] List<Vector3> wayPoint;
    [SerializeField] int currentWayPoint = 0;
    [SerializeField] float railSpeed = 3f;
    [SerializeField] bool loopableRail = false;
    [SerializeField] bool startPath = false;

    [Header("Subject to Path")]
    [SerializeField] GameObject subject;
    [SerializeField] float distanceFromObject;
    [SerializeField] bool addSubject = false;

    [Header("ResetCamera")]
    [SerializeField] bool resetCamera = false;

    void Start()
    {
        sceneCamera = Camera.main;
        cameraRotation = new Vector3(0f, 0f, 0f);
        cameraPosition = sceneCamera.transform.position;
        panValue = 0f;
        tiltValue = 0f;
        rollValue = 0f;
    }

    void Update() 
    {
        FollowPath(wayPoint, railSpeed);
        AddSubjectToPath(subject, distanceFromObject);
        ResetCamera(resetCamera);

        KeyboardCamControl();
    }


    public void Pan(float _panValue)
    {
        Vector3 startRotation = cameraRotation;
        cameraRotation.y = _panValue;
        UpdateRotation(cameraRotation);

        //update local variable
        this.panValue = _panValue;
    }

    public void Tilt(float _tiltValue)
    {
        Vector3 startRotation = cameraRotation;
        cameraRotation.x = _tiltValue;
        UpdateRotation(cameraRotation);

        this.tiltValue = _tiltValue;
    }

    public void Roll(float _rollValue)
    {
        Vector3 startRotation = cameraRotation;
        cameraRotation.z = _rollValue;
        UpdateRotation(cameraRotation);

        this.rollValue = _rollValue;
    }

    public void Crane(float _cameraHeight)
    {
        cameraPosition.y = _cameraHeight;
        UpdatePosition(cameraPosition);
        
        this.cameraHeight = _cameraHeight;
    }

    public void ChangeLength(float _cameraFL)
    {
        this.cameraFL = _cameraFL;
        ApplyLength(_cameraFL);
    }

    void FollowPath(List<Vector3> _wayPoint, float _speed)
    {
        //moveTowards waypoints of the array until the end
        if(startPath == true)
        {
            Vector3 currentPosition = sceneCamera.transform.position;
            float step = _speed * Time.deltaTime;
            
            sceneCamera.transform.position = Vector3.MoveTowards(currentPosition, 
                                                                wayPoint[currentWayPoint], 
                                                                step);
            cameraPosition = sceneCamera.transform.position;

            if(sceneCamera.transform.position == wayPoint[currentWayPoint])
            {
                currentWayPoint++;
            }

            //if Loopable reset the path
            if(loopableRail)
            {
                currentWayPoint = 0;
            }
            else if(currentWayPoint == wayPoint.Count)
            {
                //if not loopable and in final waypoint: stop it
                startPath = false;
                currentWayPoint = 0;
            }
        }
    }

    public void StartPath()
    {
        //check if it's already true
        switch(startPath)
        {
            case false:
                startPath = true;
                break;
                
            case true:
                startPath = false;
                break;
        }
    }

    void AddSubjectToPath(GameObject _subject, float _distanceFromObject)
    {
        if(addSubject)
        {
            Vector3 newPoint = _subject.transform.position + new Vector3(0f,0f,-_distanceFromObject);
            wayPoint.Add(newPoint);

            addSubject = false;
        }
    }

    void ResetCamera(bool _resetCamera)
    {
        if(_resetCamera)
        {
            cameraPosition = new Vector3(1f,1f,-20f);
            UpdatePosition(cameraPosition);
            resetCamera = false;
        }
        
    }

    void UpdateRotation(Vector3 _cameraRotation)
    {
        //apply Rotations to the main camera Transform
        sceneCamera.transform.rotation = Quaternion.Euler(_cameraRotation);
    }
    void UpdatePosition(Vector3 _cameraPosition)
    {
        sceneCamera.transform.position = new Vector3(_cameraPosition.x,_cameraPosition.y,_cameraPosition.z);
    }
    void ApplyLength(float _focalLength)
    {
        sceneCamera.focalLength = _focalLength;
    }

    void KeyboardCamControl()
    {
        switch(true)
        {
            //Pan
            case bool input when Input.GetKey(KeyCode.A):
                Pan(panValue -= rotSpeed * Time.deltaTime);
                break;
            case bool input when Input.GetKey(KeyCode.D):
                Pan(panValue += rotSpeed * Time.deltaTime);
                break;
            
            //Tilt
            case bool input when Input.GetKey(KeyCode.W):
                Tilt(tiltValue -= rotSpeed * Time.deltaTime);
                break;
            case bool input when Input.GetKey(KeyCode.S):
                Tilt(tiltValue += rotSpeed * Time.deltaTime);
                break;
            
            //Roll
            case bool input when Input.GetKey(KeyCode.J):
                Roll(rollValue -= rotSpeed * Time.deltaTime);
                break;
            case bool input when Input.GetKey(KeyCode.L):
                Roll(rollValue += rotSpeed * Time.deltaTime);
                break;
            
            //Crane
            case bool input when Input.GetKey(KeyCode.K):
                Crane(cameraHeight -= rotSpeed * Time.deltaTime);
                break;
            case bool input when Input.GetKey(KeyCode.I):
                Crane(cameraHeight += rotSpeed * Time.deltaTime);
                break;
            
            //Reset
            case bool input when Input.GetKey(KeyCode.Alpha0):
                resetCamera = true;
                break;

            //StartPath
            case bool input when Input.GetKey(KeyCode.Space):
                StartPath();
                break;

            //Zoom
            case bool input when Input.GetKey(KeyCode.N):
                ChangeLength(cameraFL -= rotSpeed * Time.deltaTime);
                break;
            case bool input when Input.GetKey(KeyCode.M):
                ChangeLength(cameraFL += rotSpeed * Time.deltaTime);
                break;

            //UpdatePos
            case bool input when Input.GetKey(KeyCode.Alpha9):
                UpdatePosition(cameraPosition);
                break;
        }
    }

}
