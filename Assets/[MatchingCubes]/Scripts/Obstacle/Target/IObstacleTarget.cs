using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleTarget
{
    bool IsHit { get; }
    void Hit(IObstacle obstacle);
}
