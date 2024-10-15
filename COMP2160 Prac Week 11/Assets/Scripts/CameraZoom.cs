using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    private Actions actions;
    private InputAction scrollAction;

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
        Debug.Log(scrollAction.ReadValue<float>());
    }
}
