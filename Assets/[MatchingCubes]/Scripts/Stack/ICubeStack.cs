using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICubeStack 
{
    void AddToStack(Cube cube);
    void RemoveFromStack(Cube cube);     
}
