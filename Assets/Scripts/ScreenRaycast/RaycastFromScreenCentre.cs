using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaycastFromScreenCentre : MonoBehaviour
{
    [Tooltip("The physics layers to try to hit with this raycast")]
    [SerializeField] private LayerMask hitLayer;

    [Tooltip("The maximum distance this raycast can travel")]
    [SerializeField] private float maxDistance;

    //Hold a reference to our camera selector so we know which camera is in use
    private CameraSelector cameraSelector;

    // Protected = like private but child scripts can see
    // Virtual = lets a child script override this function with its own version
    protected virtual void Start()
    {
        cameraSelector = FindObjectOfType<CameraSelector>();
    }

    public RaycastHit TryToHit()
    {
        // A struct cannot be "null", so we initialise an empty struct instead
        RaycastHit hit = new RaycastHit();

        // Get the currently used camera
        Camera camera = cameraSelector.GetCamera();

        // Use half the camera width and height to determine the screen centre and cast a rya from there
        Ray ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth, camera.pixelHeight) * 0.5f);

        if (Physics.Raycast(ray, out hit, maxDistance, hitLayer))
        {
            return hit;
        }

        // If we hit nothing, record the furthest point we 'could' have hit
        hit.point = ray.origin + ray.direction * maxDistance;

        // Then we can return the otherwise empty hit
        return hit;
    }
}
