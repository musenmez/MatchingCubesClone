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

    private CubeType _lastCubeType = CubeType.None;
    public CubeType LastCubeType { get => _lastCubeType; private set => _lastCubeType = value; }

    public Cube BottomCube { get; private set; }  

    public Transform StackParent => _stackParent;
    [SerializeField] private Transform _stackParent;

    protected const float OFFSET = 1f;

    [HideInInspector]
    public UnityEvent OnStackUpdated = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLastCubeTypeChanged = new UnityEvent();

    public virtual void AddToStack(Cube cube)
    {
        if (Cubes.Contains(cube))
            return;

        cube.transform.SetParent(StackParent);
        cube.transform.localPosition = StackLocalBottomPosition;

        Cubes.Add(cube);
        SetTopPosition();
        AddOffsetToStack();

        BottomCube = Cubes[Cubes.Count - 1];
        CheckLastCubeType();
        OnStackUpdated.Invoke();
    }

    public virtual void RemoveFromStack(Cube cube)
    {
        if (!Cubes.Contains(cube))
            return;
        
        Cubes.Remove(cube);
        SetBottomPosition();

        BottomCube = Cubes.Count == 0 ? null : Cubes[Cubes.Count - 1];
        CheckLastCubeType();
        OnStackUpdated.Invoke();     
    }

    protected abstract void AddOffsetToStack();

    private void SetTopPosition() 
    {
        Vector3 worldPosition = StackParent.InverseTransformPoint(StackLocalBottomPosition) + Cubes.Count * OFFSET * Vector3.up;
        StackLocalTopPosition = StackParent.TransformPoint(worldPosition);      
    }

    private void SetBottomPosition() 
    {
        Vector3 worldPosition = StackParent.InverseTransformPoint(StackLocalTopPosition) - Cubes.Count * OFFSET * Vector3.up;
        StackLocalBottomPosition = StackParent.TransformPoint(worldPosition);       
    }

    private void CheckLastCubeType()
    {
        CubeType cubeType = BottomCube != null ? BottomCube.CubeType : CubeType.None;
        if (LastCubeType != cubeType)
        {
            LastCubeType = cubeType;
            OnLastCubeTypeChanged.Invoke();
        }
    }
}
