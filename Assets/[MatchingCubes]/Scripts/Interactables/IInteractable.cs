using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool IsInteracted { get; }

    void Interact(Interactor interactor);
}
