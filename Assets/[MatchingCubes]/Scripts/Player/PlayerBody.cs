using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBody : MonoBehaviour
{
    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;

    public Transform Body => _body;
    [SerializeField] private Transform _body;

    private const float MOVEMENT_DURATION = 0.25f;
    private const Ease MOVEMENT_TWEEN_EASE = Ease.OutBack;
    private const string MOVEMENT_TWEEN_ID_SUFFIX = "MovementTweenID";

    private string _movementTweenID;

    private void Awake()
    {
        _movementTweenID = Body.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
    }

    private void OnEnable()
    {
        PlayerStack.OnOffsetApplied.AddListener(UpdatePlayerBodyPosition);
    }

    private void OnDisable()
    {
        PlayerStack.OnOffsetApplied.RemoveListener(UpdatePlayerBodyPosition);
    }

    private void UpdatePlayerBodyPosition()
    {
        Vector3 localPosition = Body.localPosition;
        localPosition.y += PlayerStack.OFFSET;        
        Utilities.LocalMovementTween(Body, localPosition, MOVEMENT_DURATION, _movementTweenID, MOVEMENT_TWEEN_EASE);
    }    
}
