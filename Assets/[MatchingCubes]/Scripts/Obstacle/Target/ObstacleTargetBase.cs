using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ObstacleTargetBase : MonoBehaviour, IObstacleTarget
{
    public virtual bool CanHittable => !IsHit;
    public bool IsHit { get; protected set; }
    public IObstacle Obstacle { get; protected set; }
    [HideInInspector]
    public UnityEvent OnHit = new UnityEvent();

    public virtual void Hit(IObstacle obstacle)
    {
        if (!CanHittable)
            return;

        IsHit = true;
        Obstacle = obstacle;
        HitAction();
        OnHit.Invoke();
    }

    protected abstract void HitAction();
}
