using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGate : InteractableBase
{
    public override void InteractAction()
    {
        EventManager.OnOrderGateInteracted.Invoke();
    }
}
