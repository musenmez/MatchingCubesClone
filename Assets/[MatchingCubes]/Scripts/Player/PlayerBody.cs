using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBody : MonoBehaviour
{
    private Collider _collider;
    private Collider Collider => _collider == null ? _collider = Body.GetComponentInChildren<Collider>() : _collider;

    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = Body.GetComponentInChildren<Rigidbody>() : _rigidbody;

    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;

    public Transform Body => _body;
    [SerializeField] private Transform _body;

    private const float MOVEMENT_SPEED = 0.1f;
    private const Ease MOVEMENT_TWEEN_EASE = Ease.Linear;
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

        Vector3 localPosition = PlayerStack.Cubes[0].transform.localPosition;
        localPosition.y += PlayerStack.OFFSET;
        Body.transform.localPosition = localPosition;
        //Utilities.LocalMovementTween(Body, localPosition, MOVEMENT_SPEED, _movementTweenID, MOVEMENT_TWEEN_EASE, false, () => OnStackMovementStarted(), () => OnStackMovementCompleted());
    }    

    private void OnStackMovementStarted() 
    {
        Collider.isTrigger = true;
        Rigidbody.isKinematic = true;
    }

    private void OnStackMovementCompleted() 
    {
        Collider.isTrigger = false;
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
