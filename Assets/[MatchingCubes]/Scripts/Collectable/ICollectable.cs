using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    bool IsCollected { get; }
    void Collect(Collector collector);
}
