using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : CollectableBase
{
    public CubeType CubeType => _cubeType;

    [SerializeField] private CubeType _cubeType;
    
}
