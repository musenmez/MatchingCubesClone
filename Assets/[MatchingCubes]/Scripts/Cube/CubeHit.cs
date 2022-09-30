using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeHit : ObstacleTargetBase
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody>() : _rigidbody;

    private Cube _cube;
    private Cube Cube => _cube == null ? _cube = GetComponentInParent<Cube>() : _cube;

    public override bool CanHittable => !IsHit && !Cube.IsMatched;
   
    private const float OBSTACLE_DESTROY_FORCE = 20f;
    private const float OBSTACLE_DESTROY_RADIUS = 4f;
    private const float OBSTACLE_DESTROY_UPWARDS_MODIFIER = 1f;

    private const float HIT_FORCE = 10f;

    public override void Hit()
    {
        if (FeverModeManager.Instance.IsFeverModeEnabled)
        {
            DestroyObstacles();
            return;
        }

        if (!CanHittable)
            return;

        base.Hit();
    }

    protected override void HitAction()
    {
        transform.SetParent(null);
        Rigidbody.isKinematic = false;
        Rigidbody.constraints = RigidbodyConstraints.None;
        Rigidbody.AddForce(Vector3.back * HIT_FORCE, ForceMode.Impulse);
    }   

    private void DestroyObstacles() 
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, OBSTACLE_DESTROY_RADIUS);
        foreach (Collider hit in colliders)
        {
            Obstacle obstacle = hit.GetComponentInParent<Obstacle>();
            if (obstacle != null) 
            {
                obstacle.DestroyObstacle(OBSTACLE_DESTROY_FORCE, explosionPos, OBSTACLE_DESTROY_RADIUS, OBSTACLE_DESTROY_UPWARDS_MODIFIER);
            }                
        }
    }
}
