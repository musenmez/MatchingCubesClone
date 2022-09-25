using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IObstacleTarget obstacleTarget = other.GetComponentInParent<IObstacleTarget>();
        if (obstacleTarget != null)
        {
            obstacleTarget.Hit();
        }
    } 
}