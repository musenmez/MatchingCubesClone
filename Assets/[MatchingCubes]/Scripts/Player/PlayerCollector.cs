using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollector : Collector
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    private void OnEnable()
    {
        Player.OnPlayerFailed.AddListener(() => CanCollect = false);
        Player.OnPlayerSucceeded.AddListener(() => CanCollect = false);
    }

    private void OnDisable()
    {
        Player.OnPlayerFailed.RemoveListener(() => CanCollect = false);
        Player.OnPlayerSucceeded.RemoveListener(() => CanCollect = false);
    }
}
