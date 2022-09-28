using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpFloor : InteractableBase
{
    public override void InteractAction()
    {
        EventManager.OnSpeedUpFloorInteracted.Invoke();
    }
}
