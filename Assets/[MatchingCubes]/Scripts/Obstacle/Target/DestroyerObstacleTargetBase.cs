using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyerObstacleTargetBase : ObstacleTargetBase
{
    protected const float OBSTACLE_DESTROY_FORCE = 20f;
    protected const float OBSTACLE_DESTROY_RADIUS = 4f;
    protected const float OBSTACLE_DESTROY_UPWARDS_MODIFIER = 1f;

    public override void Hit(IObstacle obstacle)
    {
        if (FeverModeManager.Instance.IsFeverModeEnabled)
        {
            DestroyObstacles();
            return;
        }

        if (!CanHittable)
            return;

        base.Hit(obstacle);
    }   

    private void DestroyObstacles()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, OBSTACLE_DESTROY_RADIUS);
        foreach (Collider hit in colliders)
        {
            DestroyableObstacleBase obstacle = hit.GetComponentInParent<DestroyableObstacleBase>();
            if (obstacle != null)
            {
                obstacle.DestroyObstacle(OBSTACLE_DESTROY_FORCE, explosionPos, OBSTACLE_DESTROY_RADIUS, OBSTACLE_DESTROY_UPWARDS_MODIFIER);
            }
        }
    }
}
