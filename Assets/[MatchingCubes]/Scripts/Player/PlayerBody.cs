using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBody : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = Body.GetComponentInChildren<Rigidbody>() : _rigidbody;

    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;

    public Transform Body => _body;
    [SerializeField] private Transform _body;

    private const float MOVEMENT_DURATION = PlayerStack.MOVEMENT_DURATION;
    private const Ease MOVEMENT_TWEEN_EASE = PlayerStack.MOVEMENT_TWEEN_EASE;
    private const string MOVEMENT_TWEEN_ID_SUFFIX = "MovementTweenID";

    private string _movementTweenID;

    private void Awake()
    {
        _movementTweenID = Body.GetInstanceID() + MOVEMENT_TWEEN_ID_SUFFIX;
    }

    private void OnEnable()
    {
        PlayerStack.OnOffsetApplied.AddListener(UpdatePlayerBodyPosition);
        EventManager.OnRampJumpingStarted.AddListener(OnJumpingStarted);
        EventManager.OnRampJumpingCompleted.AddListener(OnJumpingCompleted);
    }

    private void OnDisable()
    {
        PlayerStack.OnOffsetApplied.RemoveListener(UpdatePlayerBodyPosition);
        EventManager.OnRampJumpingStarted.RemoveListener(OnJumpingStarted);
        EventManager.OnRampJumpingCompleted.RemoveListener(OnJumpingCompleted);
    }

    private void UpdatePlayerBodyPosition()
    {
        if (PlayerStack.Cubes.Count < 1)
            return;

        Vector3 localPosition = PlayerStack.Cubes[PlayerStack.Cubes.Count - 1].transform.localPosition;
        localPosition.y += PlayerStack.OFFSET * PlayerStack.Cubes.Count;       
        Utilities.LocalMovementTween(Body, localPosition, MOVEMENT_DURATION, _movementTweenID, MOVEMENT_TWEEN_EASE, false, OnStackMovementStarted, OnStackMovementCompleted);
    }    

    private void OnStackMovementStarted() 
    {       
        Rigidbody.isKinematic = true;
    }

    private void OnStackMovementCompleted() 
    {
        if (FeverModeManager.Instance.IsFeverModeEnabled && PlayerStack.Cubes.Count == 0)
            return;
        
        Rigidbody.isKinematic = false;
    }

    private void OnJumpingStarted() 
    {
        if (PlayerStack.Cubes.Count != 0)
            return;

        Rigidbody.isKinematic = true;
    }

    private void OnJumpingCompleted() 
    {        
        Rigidbody.isKinematic = false;
    }
}
