using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cube Data", menuName = "Cube Data")]
public class CubeData : ScriptableObject
{
    public CubeType CubeType;
    public Color TrailColor;
    public GameObject CubePrefab;
}
