using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour
{
    private PickItem currentPickable;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && currentPickable != null)
        {
            currentPickable.Interact();
            currentPickable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PickItem item))
        {
            currentPickable = item;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PickItem item) && item == currentPickable)
        {
            currentPickable = null;
        }
    }
}
