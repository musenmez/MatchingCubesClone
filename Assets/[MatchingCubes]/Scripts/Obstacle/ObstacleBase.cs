using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour, IObstacle
{
    public ObstacleType ObstacleType => _obstacleType;
    [SerializeField] private ObstacleType _obstacleType;

    protected virtual void OnTriggerEnter(Collider other)
    {
        CheckObstacle(other);
    }

    protected virtual void CheckObstacle(Collider collider)
    {
        IObstacleTarget obstacleTarget = collider.GetComponentInParent<IObstacleTarget>();
        if (obstacleTarget != null)
        {
            obstacleTarget.Hit(this);
        }
    }   
}

public enum ObstacleType 
{
    None = 0,
    Cube = 10,
    Lava = 20,
}
