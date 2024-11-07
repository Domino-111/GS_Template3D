using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Gives us access to UnityEvent

public class EventOnInput : MonoBehaviour
{
    [SerializeField] private KeyCode triggerKey;
    [SerializeField] private UnityEvent onTrigger;

    private void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            onTrigger.Invoke();
        }
    }
}
