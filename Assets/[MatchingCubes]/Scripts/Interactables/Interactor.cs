using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
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
        IInteractable interactable = collider.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact(this);
        }
    }
}
