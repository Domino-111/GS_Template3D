using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : RaycastFromScreenCentre
{
    public void TryToInteract()
    {
        RaycastHit hit = TryToHit();

        if (hit.collider)
        {
            if (hit.collider.TryGetComponent<Interactible>(out Interactible i))
            {
                i.Interact();
            }
        }
    }
}
