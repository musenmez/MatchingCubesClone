using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeObstacleTarget : ObstacleTargetBase
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody>() : _rigidbody;

    private Cube _cube;
    private Cube Cube => _cube == null ? _cube = GetComponentInParent<Cube>() : _cube;

    public override bool CanHittable => !IsHit && !Cube.IsMatched;

    private const float FORCE = 5f;

    protected override void HitAction()
    {
        transform.SetParent(null);
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(Vector3.back * FORCE, ForceMode.Impulse);
    }
}
