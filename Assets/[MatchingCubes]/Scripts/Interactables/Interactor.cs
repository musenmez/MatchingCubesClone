using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private bool _canInteract = true;
    public bool CanInteract { get => _canInteract; protected set => _canInteract = value; }

    public Transform Body => _body;
    [SerializeField] private Transform _body;    

    private void OnTriggerEnter(Collider other)
    {
        CheckInteraction(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckInteraction(collision.collider);
    }

    private void CheckInteraction(Collider collider) 
    {
        if (!CanInteract)
            return;

        IInteractable interactable = collider.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact(this);
        }
    }
}
