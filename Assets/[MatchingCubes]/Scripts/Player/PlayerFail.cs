using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFail : ObstacleTargetBase
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInChildren<Rigidbody>() : _rigidbody;

    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    private const float FORCE = 35f;

    protected override void HitAction()
    {
        PushPlayerBack();
        Player.TriggerFail();
    }    

    private void PushPlayerBack() 
    {
        Rigidbody.isKinematic = false;
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        Rigidbody.AddForce(Vector3.back * FORCE, ForceMode.Impulse);
    }    
}
