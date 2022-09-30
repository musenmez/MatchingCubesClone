using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGate : InteractableBase
{
    public override void InteractAction()
    {
        EventManager.OnRandomGateInteracted.Invoke();
    }
}
