using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    private Actions actions;
    private InputAction scrollAction;

    [SerializeField] private float zoomSpeed = 1;

    void Awake()
    {
        actions = new Actions();
        scrollAction = actions.camera.zoom;
    }

    void OnEnable()
    {
        actions.camera.Enable();
    }

    void OnDisable()
    {
        actions.camera.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Camera cam = Camera.main;
        if(cam.orthographic)
        {
            cam.orthographicSize -= scrollAction.ReadValue<float>()/1200 * zoomSpeed;
        }
        else
        {
            cam.fieldOfView -= scrollAction.ReadValue<float>()/120 * zoomSpeed;
        }
    }
}
