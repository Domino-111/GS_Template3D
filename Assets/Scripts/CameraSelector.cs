using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public enum Selection
    {
        FirstPerson,
        ThirdPerson
    }

    [Tooltip("What camera mode the game should start in")]
    [SerializeField] private Selection selection;

    //Hold our first person camera script
    private FirstPersonCamera firstPersonCamera;

    //Hold our third person camera script
    private ThirdPersonCamera thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        //Lock the mouse cursor to the centre of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //find the two camera scripts in the scene
        firstPersonCamera = FindObjectOfType<FirstPersonCamera>();
        thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();

        //set up our starting camera
        SelectCamera(selection);
    }


    /// <summary>
    /// Select a new camera mode and apply the camera settings
    /// </summary>
    /// <param name="selection"></param>
    public void SelectCamera(Selection selection)
    {
        //set our current selection to match the incoming selection
        this.selection = selection;

        //based on the selection, enable/disable the two cameras
        switch (selection)
        {
            case Selection.FirstPerson:
                firstPersonCamera.ToggleActive(true);
                thirdPersonCamera.ToggleActive(false);
                break;
            case Selection.ThirdPerson:
                thirdPersonCamera.ToggleActive(true);
                firstPersonCamera.ToggleActive(false);
                break;
        }
    }

    /// <summary>
    /// Get the current type of camera in use.
    /// </summary>
    /// <returns></returns>
    public Selection GetCameraSelection()
    {
        return selection;
    }

    /// <summary>
    /// Get the transform of the camera which is currently in use.
    /// </summary>
    /// <returns></returns>
    public Transform GetCameraTransform()
    {
        //Depending on the selection, get the transform of the currently-used camera
        switch (selection)
        {
            case Selection.FirstPerson:
                return firstPersonCamera.transform;
            case Selection.ThirdPerson:
                return thirdPersonCamera.transform;
            default:
                return null;
        }
    }
}
