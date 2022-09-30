using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Animator Animator => _animator == null ? _animator = GetComponentInChildren<Animator>() : _animator;

    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;

    private PlayerStatus _playerStatus;
    private PlayerStatus PlayerStatus => _playerStatus == null ? _playerStatus = GetComponentInParent<PlayerStatus>() : _playerStatus;

    private const string RUN_PARAMETER = "Run";
    private const string FAIL_PARAMETER = "Fail";
    private const string WIN_PARAMETER = "Win";
    private const string JUMP_PARAMETER = "Jump";
    private const string FALL_PARAMETER = "Fall";
    private const string FLY_PARAMETER = "Fly";
    private const string CUBE_IDLE_PARAMETER = "CubeIdle";

    private void OnEnable()
    {
        Player.OnPlayerActivated.AddListener(() => SetTrigger(RUN_PARAMETER));
        Player.OnPlayerFailed.AddListener(() => SetTrigger(FAIL_PARAMETER));
        Player.OnPlayerSucceeded.AddListener(() => SetTrigger(WIN_PARAMETER));
        PlayerStack.OnStackIncreased.AddListener(() => SetTrigger(JUMP_PARAMETER));
        PlayerStatus.OnPlayerStatusChanged.AddListener(CheckPlayerStatus);
    }

    private void OnDisable()
    {
        Player.OnPlayerActivated.RemoveListener(() => SetTrigger(RUN_PARAMETER));
        Player.OnPlayerFailed.RemoveListener(() => SetTrigger(FAIL_PARAMETER));
        Player.OnPlayerSucceeded.RemoveListener(() => SetTrigger(WIN_PARAMETER));
        PlayerStack.OnStackIncreased.RemoveListener(() => SetTrigger(JUMP_PARAMETER));
        PlayerStatus.OnPlayerStatusChanged.RemoveListener(CheckPlayerStatus);
    }

    private void CheckPlayerStatus() 
    {
        switch (PlayerStatus.CurrentPlayerStatus)
        {
            case PlayerStatusType.Flying:
                SetTrigger(FLY_PARAMETER);
                break;

            case PlayerStatusType.OnAir:
                SetTrigger(FALL_PARAMETER);
                break;

            case PlayerStatusType.OnCube:
                SetTrigger(CUBE_IDLE_PARAMETER);
                break;

            case PlayerStatusType.OnGround:
                SetTrigger(RUN_PARAMETER);
                break;

            default:
                break;
        }
    }

    private void SetTrigger(string parameter) 
    {
        Animator.SetTrigger(parameter);
    }
}
