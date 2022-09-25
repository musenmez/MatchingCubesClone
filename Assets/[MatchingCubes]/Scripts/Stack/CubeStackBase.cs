using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CubeStackBase : MonoBehaviour, ICubeStack
{
    private List<Cube> _cubes = new List<Cube>();
    public List<Cube> Cubes { get => _cubes; protected set => _cubes = value; }

    private Vector3 _stackLocalTopPosition = Vector3.zero;
    public Vector3 StackLocalTopPosition { get => _stackLocalTopPosition; protected set => _stackLocalTopPosition = value; }
    
    private Vector3 _stackLocalBottomPosition = Vector3.zero;
    public Vector3 StackLocalBottomPosition { get => _stackLocalBottomPosition; protected set => _stackLocalBottomPosition = value; }
    
    public Transform StackParent => _stackParent;
    [SerializeField] private Transform _stackParent;

    protected const float OFFSET = 1f;

    [HideInInspector]
    public UnityEvent OnStackUpdated = new UnityEvent();    

    public void AddToStack(Cube cube)
    {
        if (Cubes.Contains(cube))
            return;

        cube.transform.SetParent(StackParent);
        Cubes.Add(cube);

        SetTopPosition();
        UpdateStack();
    }

    public void RemoveFromStack(Cube cube)
    {
        if (!Cubes.Contains(cube))
            return;

        cube.transform.SetParent(null);
        Cubes.Remove(cube);

        SetBottomPosition();
        UpdateStack();
    }

    protected virtual void UpdateStack() 
    {
        OnStackUpdated.Invoke();
    }

    private void SetTopPosition() 
    {
        Vector3 worldPosition = StackLocalBottomPosition + Cubes.Count * OFFSET * Vector3.up;
        StackLocalTopPosition = StackParent.TransformPoint(worldPosition);
    }

    private void SetBottomPosition() 
    {
        Vector3 worldPosition = StackLocalTopPosition - Cubes.Count * OFFSET * Vector3.up;
        StackLocalBottomPosition = StackParent.TransformPoint(worldPosition);
    }   
}
