using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Linq;

public class PlayerStack : CubeStackBase
{
    public const float MOVEMENT_DURATION = 0.25f;
    public const Ease MOVEMENT_TWEEN_EASE = Ease.Linear;
    private const string MOVEMENT_TWEEN_ID_SUFFIX = "MovementTweenID";

    [HideInInspector]
    public UnityEvent OnOffsetApplied = new UnityEvent();

    private void OnEnable()
    {
        EventManager.OnOrderGateInteracted.AddListener(OrderStack);
        EventManager.OnRandomGateInteracted.AddListener(RandomizeStack);
    }

    private void OnDisable()
    {
        EventManager.OnOrderGateInteracted.RemoveListener(OrderStack);
        EventManager.OnRandomGateInteracted.RemoveListener(RandomizeStack);
    }

    public override void RemoveFromStack(Cube cube)
    {
        if (!Cubes.Contains(cube))
            return;

        string tweenID = cube.transform.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
        DOTween.Kill(tweenID);

        base.RemoveFromStack(cube);
    }

    public void CompleteStackMovementTween()
    {
        foreach (var cube in Cubes)
        {
            string tweenID = cube.transform.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
            DOTween.Complete(tweenID);
        }
    }

    protected override void AddOffsetToStack()
    {
        UpdateStackPosition();
        OnOffsetApplied.Invoke();
    }   

    private void UpdateStackPosition() 
    {
        if (Cubes.Count <= 1)
            return;

        int lastIndex = Cubes.Count - 1;
        for (int i = Cubes.Count - 2; i >= 0 ; i--)
        {
            Vector3 localPosition = Cubes[lastIndex].transform.localPosition;
            localPosition.y += (OFFSET * (lastIndex - i));            

            string tweenID = Cubes[i].transform.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
            Utilities.LocalMovementTween(Cubes[i].transform, localPosition, MOVEMENT_DURATION, tweenID, MOVEMENT_TWEEN_EASE, onStart: Cubes[i].OnStackMovementStarted, onComplete: Cubes[i].OnStackMovementCompleted);
        }
    }

    private void RandomizeStack()
    {
        CompleteStackMovementTween();

        List<Vector3> indexPositions = new List<Vector3>(Cubes.Select(cube => cube.transform.position));
        Cubes.Shuffle();

        for (int i = 0; i < Cubes.Count; i++)
        {
            Cubes[i].transform.position = indexPositions[i];
        }

        UpdateStack();
    }

    private void OrderStack()
    {
        CompleteStackMovementTween();

        List<Vector3> indexPositions = new List<Vector3>(Cubes.Select(cube => cube.transform.position));
        List<CubeType> orderedTypes = new List<CubeType>();
        List<Cube> orderedCubes = new List<Cube>();
       
        foreach (var cube in Cubes)
        {
            if (orderedTypes.Contains(cube.CubeType))
                continue;

            orderedTypes.Add(cube.CubeType);
            orderedCubes.AddRange(Cubes.Where(x => x.CubeType == cube.CubeType));
        }
        
        Cubes = orderedCubes;
        for (int i = 0; i < Cubes.Count; i++)
        {
            Cubes[i].transform.position = indexPositions[i];
        }

        UpdateStack();
    }
}
