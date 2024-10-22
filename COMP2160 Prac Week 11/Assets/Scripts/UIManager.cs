/**
 * A singleton class to allow point-and-click movement of the marble.
 * 
 * It publishes a TargetSelected event which is invoked whenever a new target is selected.
 * 
 * Author: Malcolm Ryan
 * Version: 1.0
 * For Unity Version: 2022.3
 */

using UnityEngine;
using UnityEngine.InputSystem;

// note this has to run earlier than other classes which subscribe to the TargetSelected event
[DefaultExecutionOrder(-100)]
public class UIManager : MonoBehaviour
{
#region UI Elements
    [SerializeField] private Transform crosshair;
    [SerializeField] private Transform target;
#endregion 

#region Singleton
    static private UIManager instance;
    static public UIManager Instance
    {
        get { return instance; }
    }
#endregion 

#region Actions
    private Actions actions;
    private InputAction mouseAction;
    private InputAction deltaAction;
    private InputAction selectAction;
#endregion

#region Events
    public delegate void TargetSelectedEventHandler(Vector3 worldPosition);
    public event TargetSelectedEventHandler TargetSelected;
#endregion

#region Other
    private Vector3 mouseLoc;
    public Vector3 CrosshairPos;
#endregion

#region Init & Destroy
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one UIManager in the scene.");
        }

        instance = this;

        actions = new Actions();
        mouseAction = actions.mouse.position;
        deltaAction = actions.mouse.delta;
        selectAction = actions.mouse.select;

        Cursor.visible = false;
        target.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        actions.mouse.Enable();
    }

    void Start()
    {
        mouseLoc = new Vector3(mouseAction.ReadValue<Vector2>().x,mouseAction.ReadValue<Vector2>().y, 0);
    }

    void OnDisable()
    {
        actions.mouse.Disable();
    }
#endregion Init

#region Update
    void Update()
    {
        MoveCrosshair();
        SelectTarget();
    }

    private void MoveCrosshair() 
    {
        Camera cam = Camera.main;
        Vector3 mousePos = new Vector3(mouseAction.ReadValue<Vector2>().x,mouseAction.ReadValue<Vector2>().y, 0);
        //mousePos += new Vector3(deltaAction.ReadValue<Vector2>().x,deltaAction.ReadValue<Vector2>().y, 0);
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        int layerMask = 1 << 8;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 crossLoc = hit.point + Vector3.up * 0.01f;      //to avoid clipping on the Y-Axis with the ground
            crosshair.position = crossLoc;
        }
    }

    private void MoveCrosshairDelta()
    {
        Camera cam = Camera.main;
        mouseLoc += new Vector3(deltaAction.ReadValue<Vector2>().x,deltaAction.ReadValue<Vector2>().y, 0);
        Debug.Log(mouseLoc);
        Ray ray = cam.ScreenPointToRay(mouseLoc);
        RaycastHit hit;
        int layerMask = 1 << 8;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 crossLoc = hit.point + Vector3.up * 0.01f;      //to avoid clipping on the Y-Axis with the ground
            crosshair.position = crossLoc;
            CrosshairPos = crossLoc;
        }
    }

    private void SelectTarget()
    {
        if (selectAction.WasPerformedThisFrame())
        {
            // set the target position and invoke 
            target.gameObject.SetActive(true);
            target.position = crosshair.position;     
            TargetSelected?.Invoke(target.position);       
        }
    }

#endregion Update

}
