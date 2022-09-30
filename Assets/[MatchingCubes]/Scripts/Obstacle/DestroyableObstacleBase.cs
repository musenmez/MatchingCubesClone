using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObstacleBase : ObstacleBase
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInChildren<Rigidbody>() : _rigidbody;

    private Collider[] _colliders;
    private Collider[] Colliders => _colliders == null ? _colliders = GetComponentsInChildren<Collider>() : _colliders;

    public bool IsDestroyed { get; private set; }   

    protected override void CheckObstacle(Collider collider)
    {
        if (IsDestroyed)
            return;

        base.CheckObstacle(collider);
    }

    public void DestroyObstacle(float force, Vector3 explosionPosition, float radius, float upwardsModifier)
    {
        if (IsDestroyed)
            return;

        IsDestroyed = true;
        Rigidbody.isKinematic = false;
        SetCollidersTrigger(true);
        Rigidbody.AddExplosionForce(force, explosionPosition, radius, upwardsModifier, ForceMode.Impulse);
    }

    private void SetCollidersTrigger(bool isTrigger)
    {
        foreach (var collider in Colliders)
        {
            collider.isTrigger = isTrigger;
        }
    }
}
