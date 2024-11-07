using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [Tooltip("The transform this object should look at")]
    [SerializeField] private Transform target;

    void Update()
    {
        transform.LookAt(target);
    }
}
