using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerStack : MonoBehaviour
{
    private PlayerCollector _playerCollector;
    private PlayerCollector PlayerCollector => _playerCollector == null ? _playerCollector = GetComponentInParent<PlayerCollector>() : _playerCollector;

    [SerializeField] private Transform _playerBody;
    [SerializeField] private Transform _stackParent;

    private const string MOVEMENT_TWEEN_ID_SUFFIX = "MovementTweenID";
    private const float MOVEMENT_DURATION = 0.25f;
    private const Ease MOVEMENT_TWEEN_EASE = Ease.OutBack;

    private const float OFFSET = 1f;

    private void OnEnable()
    {
        PlayerCollector.OnCubeCollectionChanged.AddListener(UpdatePositions);
    }

    private void OnDisable()
    {
        PlayerCollector.OnCubeCollectionChanged.RemoveListener(UpdatePositions);
    }

    private void UpdatePositions()
    {
        UpdatePlayerBodyPosition();
        UpdateStackPosition();
    }

    private void UpdatePlayerBodyPosition()
    {
        Vector3 localPosition = _playerBody.localPosition;
        localPosition.y = PlayerCollector.CollectedCubes.Count * OFFSET;

        string tweenID = _playerBody.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
        SetLocalPosition(_playerBody, localPosition, MOVEMENT_DURATION, tweenID);        
    }

    private void UpdateStackPosition() 
    {
        int lastIndex = PlayerCollector.CollectedCubes.Count - 1;
        for (int i = 0; i <= lastIndex; i++)
        {
            PlayerCollector.CollectedCubes[i].transform.SetParent(_stackParent);

            Vector3 localPosition = _stackParent.InverseTransformPoint(_stackParent.position);
            localPosition.y = OFFSET * (lastIndex - i);           

            string tweenID = PlayerCollector.CollectedCubes[i].transform.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
            float duration = i == lastIndex ? 0 : MOVEMENT_DURATION;
            SetLocalPosition(PlayerCollector.CollectedCubes[i].transform, localPosition, duration, tweenID);
        }
    }    

    private void SetLocalPosition(Transform target, Vector3 localPosition, float duration, string tweenID) 
    {
        DOTween.Kill(tweenID);
        target.DOLocalMove(localPosition, duration).SetId(tweenID).SetEase(MOVEMENT_TWEEN_EASE);
    }
}
