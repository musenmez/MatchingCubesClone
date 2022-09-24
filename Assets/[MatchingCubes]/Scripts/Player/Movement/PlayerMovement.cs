using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;
    public float CurrentSpeed { get; private set; }
    
    [SerializeField] private MovementData _movementData;
    public MovementData MovementData => _movementData;

    private void Awake()
    {
        Setup();
    }

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward() 
    {
        if (!Player.CanMoveForward)
            return;

        transform.Translate(CurrentSpeed * Time.deltaTime * Vector3.forward);
    }

    private void Setup() 
    {
        CurrentSpeed = MovementData.DefaultSpeed;
    }
}
