using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableBase : MonoBehaviour, ICollectable
{
    public bool IsCollected { get; protected set; }
    public Collector Collector { get; protected set; }

    public virtual void Collect(Collector collector)
    {
        if (IsCollected || !CanCollect(collector))
            return;

        IsCollected = true;
        Collector = collector;
        CollectAction();        
    }

    protected virtual bool CanCollect(Collector collector) 
    {
        return true;
    }

    protected abstract void CollectAction();    

  
}
