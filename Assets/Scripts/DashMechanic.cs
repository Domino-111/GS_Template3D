using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMechanic : MonoBehaviour
{
    // Declaring variables

    [SerializeField] private float timer;
    [SerializeField] private bool timerFinished = true;

    [SerializeField] private float detectionDistance;

    [SerializeField] private float dashDistance;

    [SerializeField] private Transform playerPosition;

    RaycastHit hit;

    void Update()
    {


        Physics.Raycast(playerPosition.position, Vector3.forward, detectionDistance);

        // Timer activated by dash 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (timer && )
            {

            }

            timerFinished = false;

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timerFinished = true;
            }
        }
    }
}
