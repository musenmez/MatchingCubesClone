using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : CollectableBase
{
    public CubeType CubeType => _cubeType;

    [SerializeField] private CubeType _cubeType;

    [HideInInspector]
    public UnityEvent OnCollected = new UnityEvent();    

    protected override void CollectAction()
    {
        CubeCollector cubeCollector = Collector.GetComponent<CubeCollector>();      
        cubeCollector.AddCube(this);
        OnCollected.Invoke();
    }

    protected override bool CanCollect(Collector collector)
    {
        CubeCollector cubeCollector = collector.GetComponent<CubeCollector>();
        if (cubeCollector == null) return false;
        else return true;
    }
}
