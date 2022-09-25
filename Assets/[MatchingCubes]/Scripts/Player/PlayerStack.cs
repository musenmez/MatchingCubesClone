using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerStack : CubeStackBase
{ 
    [SerializeField] private Transform _playerBody;  

    private const string MOVEMENT_TWEEN_ID_SUFFIX = "MovementTweenID";
    private const float MOVEMENT_DURATION = 0.25f;
    private const Ease MOVEMENT_TWEEN_EASE = Ease.OutBack;

    private const float OFFSET = 1f;     

    protected override void UpdateStack()
    {
        UpdatePlayerBodyPosition();
        UpdateStackPosition();
        base.UpdateStack();
    }

    private void UpdatePlayerBodyPosition()
    {
        Vector3 localPosition = _playerBody.localPosition;
        localPosition.y = Cubes.Count * OFFSET;

        string tweenID = _playerBody.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
        SetLocalPosition(_playerBody, localPosition, MOVEMENT_DURATION, tweenID);        
    }

    private void UpdateStackPosition() 
    {
        int lastIndex = Cubes.Count - 1;
        for (int i = 0; i < Cubes.Count; i++)
        {
            Vector3 localPosition = StackParent.InverseTransformPoint(StackParent.position);
            localPosition.y = OFFSET * (lastIndex - i);

            string tweenID = Cubes[i].transform.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
            float duration = i == lastIndex ? 0 : MOVEMENT_DURATION;
            SetLocalPosition(Cubes[i].transform, localPosition, duration, tweenID);
        }
    }    

    private void SetLocalPosition(Transform target, Vector3 localPosition, float duration, string tweenID) 
    {
        DOTween.Kill(tweenID);
        target.DOLocalMove(localPosition, duration).SetId(tweenID).SetEase(MOVEMENT_TWEEN_EASE);
    }
}
