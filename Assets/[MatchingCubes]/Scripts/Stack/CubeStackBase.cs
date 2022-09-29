using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CubeStackBase : MonoBehaviour, ICubeStack
{
    private List<Cube> _cubes = new List<Cube>();
    public List<Cube> Cubes { get => _cubes; protected set => _cubes = value; }   

    private CubeType _lastCubeType = CubeType.None;
    public CubeType LastCubeType { get => _lastCubeType; private set => _lastCubeType = value; }

    public Cube BottomCube { get; private set; }  

    public Transform StackParent => _stackParent;
    [SerializeField] private Transform _stackParent;

    public const float OFFSET = 1f;

    [HideInInspector]
    public UnityEvent OnStackUpdated = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLastCubeTypeChanged = new UnityEvent();

    public virtual void AddToStack(Cube cube)
    {
        if (Cubes.Contains(cube))
            return;

        cube.transform.SetParent(StackParent);
        cube.transform.localPosition = Vector3.zero;

        Cubes.Add(cube);     
        AddOffsetToStack();
        UpdateStack();
    }

    public virtual void RemoveFromStack(Cube cube)
    {
        if (!Cubes.Contains(cube))
            return;
        
        Cubes.Remove(cube);       
        UpdateStack();
    }

    public virtual void UpdateStack() 
    {
        BottomCube = Cubes.Count == 0 ? null : Cubes[Cubes.Count - 1];
        CheckLastCubeType();
        OnStackUpdated.Invoke();
    }

    protected abstract void AddOffsetToStack();    

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
