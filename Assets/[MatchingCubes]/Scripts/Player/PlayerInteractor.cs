using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : Interactor
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    private void OnEnable()
    {
        Player.OnPlayerFailed.AddListener(() => CanInteract = false);
        Player.OnPlayerSucceeded.AddListener(() => CanInteract = false);
    }

    private void OnDisable()
    {
        Player.OnPlayerFailed.RemoveListener(() => CanInteract = false);
        Player.OnPlayerSucceeded.RemoveListener(() => CanInteract = false);
    }
}
