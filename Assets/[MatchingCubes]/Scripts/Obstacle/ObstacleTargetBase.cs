using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ObstacleTargetBase : MonoBehaviour, IObstacleTarget
{
    public virtual bool CanHittable => !IsHit;
    public bool IsHit { get; protected set; }

    [HideInInspector]
    public UnityEvent OnHit = new UnityEvent();

    public virtual void Hit()
    {
        if (!CanHittable)
            return;

        IsHit = true;
        HitAction();
        OnHit.Invoke();
    }

    protected abstract void HitAction();
}
