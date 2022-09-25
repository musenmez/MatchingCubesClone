using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private bool _canCollect = true;
    public bool CanCollect { get => _canCollect; private set => _canCollect = value; }

    protected virtual void OnTriggerEnter(Collider other)
    {
        CheckCollectables(other);
    }

    protected virtual void CheckCollectables(Collider other) 
    {
        if (!CanCollect)
            return;

        ICollectable collectable = other.GetComponentInParent<ICollectable>();
        if (collectable != null && !collectable.IsCollected)
        {
            collectable.Collect(this);
        }
    }
}
