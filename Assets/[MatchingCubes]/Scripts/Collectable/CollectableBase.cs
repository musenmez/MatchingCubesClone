using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableBase : MonoBehaviour, ICollectable
{
    public bool IsCollected { get; protected set; }
    public Collector Collector { get; protected set; }

    [HideInInspector]
    public UnityEvent OnCollected = new UnityEvent();

    public virtual void Collect(Collector collector)
    {
        if (IsCollected)
            return;

        IsCollected = true;
        Collector = collector;
        OnCollected.Invoke();
    }
}
