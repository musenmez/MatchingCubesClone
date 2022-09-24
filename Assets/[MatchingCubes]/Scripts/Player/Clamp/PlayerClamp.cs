using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClamp : LeanDragTranslateWithClamp
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    protected override void Update()
    {
        if (!Player.IsControllable)
            return;

        base.Update();
    }
}
