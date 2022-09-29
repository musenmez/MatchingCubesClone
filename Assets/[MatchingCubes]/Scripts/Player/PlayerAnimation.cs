using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Animator Animator => _animator == null ? _animator = GetComponentInChildren<Animator>() : _animator;

    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    private const string RUN_PARAMETER = "Run";
    private const string FAIL_PARAMETER = "Fail";

    private void OnEnable()
    {
        Player.OnPlayerActivated.AddListener(() => SetTrigger(RUN_PARAMETER));
        Player.OnPlayerFailed.AddListener(() => SetTrigger(FAIL_PARAMETER));
    }

    private void OnDisable()
    {
        Player.OnPlayerActivated.RemoveListener(() => SetTrigger(RUN_PARAMETER));
        Player.OnPlayerFailed.RemoveListener(() => SetTrigger(FAIL_PARAMETER));
    }

    private void SetTrigger(string parameter) 
    {
        Animator.SetTrigger(parameter);
    }
}
