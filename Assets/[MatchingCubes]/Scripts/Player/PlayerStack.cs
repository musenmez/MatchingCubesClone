using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerStack : CubeStackBase
{
    public Transform PlayerBody => _playerBody;
    [SerializeField] private Transform _playerBody;

    private const float MOVEMENT_DURATION = 0.25f;
    private const Ease MOVEMENT_TWEEN_EASE = Ease.OutBack;
    private const string MOVEMENT_TWEEN_ID_SUFFIX = "MovementTweenID";    

    public override void RemoveFromStack(Cube cube)
    {
        if (!Cubes.Contains(cube))
            return;

        string tweenID = cube.transform.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
        DOTween.Kill(tweenID);

        base.RemoveFromStack(cube);
    }

    protected override void AddOffsetToStack()
    {
        UpdatePlayerBodyPosition();
        UpdateStackPosition();        
    }

    private void UpdatePlayerBodyPosition()
    {
        Vector3 localPosition = PlayerBody.localPosition;
        localPosition.y += OFFSET;

        string tweenID = PlayerBody.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
        SetLocalPosition(PlayerBody, localPosition, MOVEMENT_DURATION, tweenID);        
    }

    private void UpdateStackPosition() 
    {        
        for (int i = 0; i < Cubes.Count - 1; i++)
        {
            Vector3 localPosition = Cubes[i].transform.localPosition;
            localPosition.y += OFFSET;

            string tweenID = Cubes[i].transform.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;            
            SetLocalPosition(Cubes[i].transform, localPosition, MOVEMENT_DURATION, tweenID);
        }
    }

    private void SetLocalPosition(Transform target, Vector3 localPosition, float duration, string tweenID) 
    {
        DOTween.Kill(tweenID);
        target.DOLocalMove(localPosition, duration).SetId(tweenID).SetEase(MOVEMENT_TWEEN_EASE);
    }   
}
