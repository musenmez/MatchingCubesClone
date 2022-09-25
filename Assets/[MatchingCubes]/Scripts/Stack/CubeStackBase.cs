using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CubeStackBase : MonoBehaviour, ICubeStack
{
    private List<Cube> _cubes = new List<Cube>();
    public List<Cube> Cubes { get => _cubes; protected set => _cubes = value; }
    public Transform StackParent => _stackParent;

    [SerializeField] private Transform _stackParent;

    [HideInInspector]
    public UnityEvent OnStackUpdated = new UnityEvent();

    public void AddToStack(Cube cube)
    {
        if (Cubes.Contains(cube))
            return;

        cube.transform.SetParent(StackParent);
        Cubes.Add(cube);
        UpdateStack();
    }

    public void RemoveFromStack(Cube cube)
    {
        if (!Cubes.Contains(cube))
            return;

        cube.transform.SetParent(null);
        Cubes.Remove(cube);
        UpdateStack();
    }

    protected virtual void UpdateStack() 
    {
        OnStackUpdated.Invoke();
    }
}
