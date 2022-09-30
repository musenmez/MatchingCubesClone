using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeHit : DestroyerObstacleTargetBase
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody>() : _rigidbody;

    private Cube _cube;
    private Cube Cube => _cube == null ? _cube = GetComponentInParent<Cube>() : _cube;

    public override bool CanHittable => !IsHit && !Cube.IsMatched;   
    
    private const float HIT_FORCE = 10f;

    [HideInInspector]
    public UnityEvent OnHitLava = new UnityEvent();

    protected override void HitAction()
    {
        switch (Obstacle.ObstacleType)
        {            
            case ObstacleType.Cube:
                PushBack();
                break;

            case ObstacleType.Lava:
                OnHitLava.Invoke();
                break;

            default:
                break;
        }
    }       

    private void PushBack() 
    {
        transform.SetParent(null);
        Rigidbody.isKinematic = false;
        Rigidbody.constraints = RigidbodyConstraints.None;
        Rigidbody.AddForce(Vector3.back * HIT_FORCE, ForceMode.Impulse);
    }   
}
