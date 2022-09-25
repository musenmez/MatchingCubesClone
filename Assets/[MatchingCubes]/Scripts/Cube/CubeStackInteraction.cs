using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStackInteraction : MonoBehaviour
{
    private Cube _cube;
    private Cube Cube => _cube == null ? _cube = GetComponentInParent<Cube>() : _cube;

    private CubeObstacleTarget _cubeObstacleTarget;
    private CubeObstacleTarget CubeObstacleTarget => _cubeObstacleTarget == null ? _cubeObstacleTarget = GetComponentInParent<CubeObstacleTarget>() : _cubeObstacleTarget;

    private ICubeStack _cubeStack;
    private ICubeStack CubeStack => _cubeStack == null ? _cubeStack = Cube.Collector.GetComponentInParent<ICubeStack>() : _cubeStack;

    private void OnEnable()
    {
        Cube.OnCollected.AddListener(AddToStack);
        Cube.OnMatched.AddListener(RemoveFromStack);
        CubeObstacleTarget.OnHit.AddListener(RemoveFromStack);
    }

    private void OnDisable()
    {
        Cube.OnCollected.RemoveListener(AddToStack);
        Cube.OnMatched.RemoveListener(RemoveFromStack);
        CubeObstacleTarget.OnHit.RemoveListener(RemoveFromStack);
    }

    private void AddToStack()
    {
        if (CubeStack == null)
            return;

        CubeStack.AddToStack(Cube);
    }

    private void RemoveFromStack() 
    {
        if (CubeStack == null)
            return;

        CubeStack.RemoveFromStack(Cube);
    }
}
