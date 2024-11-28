using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMechanic : MonoBehaviour
{
    // Declaring variables
    #region Time Variables
    [SerializeField] private float timer;

    [Tooltip("The cooldown time for the mechanic")]
    [SerializeField] private float timerMax;

    private bool timerFinished = true;
    #endregion

    #region Raycast Variables
    [Tooltip("The range of the raycast")]
    [SerializeField] private float detectionDistance;

    [Tooltip("The distance you wish to dash to")]
    [SerializeField] private float dashDistance;

    [SerializeField] private Transform playerPosition;

    [SerializeField] private LayerMask hitLayer;
    #endregion

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Checks if the timer is done and doesn't detect any objects in front of it
            if (timerFinished && !Physics.Raycast(playerPosition.position, new Vector3(0, 0, playerPosition.localPosition.z), detectionDistance, hitLayer))
            {
                // 
                rb.AddForce(playerPosition.forward * dashDistance, ForceMode.Impulse);
                
                // Stops from dashing infinitely
                timerFinished = false;

                // Sets timer back to max to begin again
                timer = timerMax;
            }
        }

        timer -= Time.deltaTime;

        // Detects when timer is done and allows mechanic to work again
        if (timer < 0)
        {
            timerFinished = true;
        }
    }

    // Creates a bool to stop velocity change in CustomController
    public bool IsDashing()
    {
        return timer > 4.5;
    }
}
