using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : InteractableBase
{
    public override void InteractAction()
    {
        Player.Instance.TriggerSuccess();
    }
}
