using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHit : ObstacleTargetBase
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInChildren<Rigidbody>() : _rigidbody;

    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    private const float FORCE = 35f;

    private const float OBSTACLE_DESTROY_FORCE = 20f;
    private const float OBSTACLE_DESTROY_RADIUS = 4f;
    private const float OBSTACLE_DESTROY_UPWARDS_MODIFIER = 1f;

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
        PushPlayerBack();
        Player.TriggerFail();
    }    

    private void PushPlayerBack() 
    {
        Rigidbody.isKinematic = false;
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        Rigidbody.AddForce(Vector3.back * FORCE, ForceMode.Impulse);
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
