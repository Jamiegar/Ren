using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact(gameObject);
        }
    }
}
