using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public bool IsInteracted { get; private set; }
    public Interactor Interactor { get; private set; }

    [HideInInspector]
    public UnityEvent OnInteracted = new UnityEvent();

    public void Interact(Interactor interactor)
    {
        if (IsInteracted)
            return;

        IsInteracted = true;
        Interactor = interactor;
        InteractAction();
        OnInteracted.Invoke();
    }

    public abstract void InteractAction();
}
