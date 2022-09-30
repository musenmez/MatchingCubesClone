using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

    private PlayerBodyGroundCheck _groundCheck;
    private PlayerBodyGroundCheck GroundCheck => _groundCheck == null ? _groundCheck = GetComponentInParent<PlayerBodyGroundCheck>() : _groundCheck;

    private PlayerBodyCubeCheck _cubeCheck;
    private PlayerBodyCubeCheck CubeCheck => _cubeCheck == null ? _cubeCheck = GetComponentInParent<PlayerBodyCubeCheck>() : _cubeCheck;

    private PlayerStatusType _previousPlayerStatus = PlayerStatusType.OnGround;
    public PlayerStatusType PreviousPlayerStatus { get => _previousPlayerStatus; private set => _previousPlayerStatus = value; }

    private PlayerStatusType _currentPlayerStatus = PlayerStatusType.OnGround;
    public PlayerStatusType CurrentPlayerStatus { get => _currentPlayerStatus; private set => _currentPlayerStatus = value; }

    [HideInInspector]
    public UnityEvent OnPlayerStatusChanged = new UnityEvent();

    private void OnEnable()
    {
        GroundCheck.OnGroundedStatusChanged.AddListener(CheckStatus);
        CubeCheck.OnGroundedStatusChanged.AddListener(CheckStatus);
        Player.OnPlayerJumpingStarted.AddListener(CheckStatus);
        Player.OnPlayerJumpingCompleted.AddListener(CheckStatus);
    }

    private void OnDisable()
    {
        GroundCheck.OnGroundedStatusChanged.RemoveListener(CheckStatus);
        CubeCheck.OnGroundedStatusChanged.RemoveListener(CheckStatus);
        Player.OnPlayerJumpingStarted.RemoveListener(CheckStatus);
        Player.OnPlayerJumpingCompleted.RemoveListener(CheckStatus);
    }

    private void CheckStatus() 
    {
        PlayerStatusType playerStatus;

        if (Player.IsJumping)
            playerStatus = PlayerStatusType.Flying;

        else if (GroundCheck.IsGrounded)
            playerStatus = PlayerStatusType.OnGround;

        else if (CubeCheck.IsGrounded)
            playerStatus = PlayerStatusType.OnCube;

        else
            playerStatus = PlayerStatusType.OnAir;
        
        if (CurrentPlayerStatus != playerStatus)
        {
            PreviousPlayerStatus = CurrentPlayerStatus;
            CurrentPlayerStatus = playerStatus;
            OnPlayerStatusChanged.Invoke();
        }        
    }
}

public enum PlayerStatusType 
{
    None = 0,
    OnAir = 10,
    OnCube = 20,    
    OnGround = 30,   
    Flying = 40
}
