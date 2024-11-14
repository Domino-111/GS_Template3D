using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Tooltip("How fast the camera should move as the player looks around")]
    [SerializeField] private float sensitivity;

    [Tooltip("The minimum allowed rotation around the x-axis")]
    [SerializeField] private float verticalRotationMin;

    [Tooltip("The maximum allowed rotation around the x-axis")]
    [SerializeField] private float verticalRotationMax;

    [Tooltip("The player object's transform")]
    [SerializeField] private Transform playerTransform;

    [Tooltip("The distance the camera tries to rest at from the player")]
    [SerializeField] private float cameraZoomIdeal;

    [Tooltip("The speed the camera zooms out from the player in units per sec")]
    [SerializeField] private float zoomOutSpeed;

    [Tooltip("The physics layer to check for when avoiding objects blocking the player from view")]
    [SerializeField] private LayerMask avoidLayer;

    //The transform of the game object that has the camera on it, responsible for zoom (z-position) only
    private Transform cameraTransform;

    //The transform of the game object which is the first child of the anchor, responsible for x-axis rotation only
    private Transform boomTransform;

    //Track how zoomed in or out the camera is currently
    private float currentCameraZoom;

    //Track our current rotation values
    private float currentHorizontalRotation, currentVerticalRotation;

    //Hold the camera in use by this script
    new private Camera camera;

    /// <summary>
    /// Enable or disable the associated camera by passing true or false.
    /// </summary>
    /// <param name="active"></param>
    public void ToggleActive(bool active)
    {
        camera.enabled = active;
    }

    //Awake runs once when the script first loads in the scene
    private void Awake()
    {
        //Since the third person camera has 3 game objects involved, we must look for the Camera in the child game objects
        camera = GetComponentInChildren<Camera>();

        //Set our starting rotation values
        currentHorizontalRotation = transform.localEulerAngles.y;
        currentVerticalRotation = transform.localEulerAngles.x;

        //Get the two transforms for our camera and boom
        cameraTransform = camera.transform;
        boomTransform = transform.GetChild(0);
    }

    private void Update()
    {
        GetRotationFromInput();
        ClampVerticalRotation();
        ApplyRotation();

        //Snap to the player's position
        transform.position = playerTransform.position;

        //Figure out the direction from the player to the camera
        Vector3 directionToCamera = (cameraTransform.position - transform.position).normalized;

        //If we raycast from the player to the camera and we hit an obstacle...
        if (Physics.Raycast(transform.position, directionToCamera, out RaycastHit hit, cameraZoomIdeal, avoidLayer))
        {
            //... set our current zoom based on where we hit
            currentCameraZoom = hit.distance;
        }
        else
        {
            // else, we zoom out over time towards the ideal distance
            currentCameraZoom = Mathf.MoveTowards(currentCameraZoom, cameraZoomIdeal, zoomOutSpeed * Time.deltaTime);
        }

        //Set the camera's local position using the current camera zoom
        cameraTransform.localPosition = new(0, 0, -currentCameraZoom);
    }

    /// <summary>
    /// Use the mouse inputs to adjust the current camera rotation
    /// </summary>
    private void ApplyRotation()
    {
        transform.localEulerAngles = new Vector3(0, currentHorizontalRotation, 0);
        boomTransform.localEulerAngles = new Vector3(currentVerticalRotation, 0, 0);
    }

    /// <summary>
    /// Clamp the x-axis rotation using the minimum and maximum provided
    /// </summary>
    private void GetRotationFromInput()
    {
        currentHorizontalRotation += Input.GetAxis("Mouse X") * sensitivity;
        currentVerticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;
    }

    /// <summary>
    /// Set the camera's rotation as determined
    /// </summary>
    private void ClampVerticalRotation()
    {
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, verticalRotationMin, verticalRotationMax);
    }
}
