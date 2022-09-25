using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeCollector : Collector
{
    private List<Cube> _collectedCubes = new List<Cube>();
    public List<Cube> CollectedCubes { get => _collectedCubes; private set => _collectedCubes = value; }

    [HideInInspector]
    public UnityEvent OnCubeCollectionChanged = new UnityEvent();

    public void AddCube(Cube cube)
    {
        if (CollectedCubes.Contains(cube))
            return;

        CollectedCubes.Add(cube);
        OnCubeCollectionChanged.Invoke();
    }

    public void RemoveCube(Cube cube)
    {
        if (!CollectedCubes.Contains(cube))
            return;

        CollectedCubes.Remove(cube);
        OnCubeCollectionChanged.Invoke();
    }
}
