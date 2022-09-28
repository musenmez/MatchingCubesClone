using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;
    public float CurrentSpeed { get; private set; }
    
    [SerializeField] private MovementData _movementData;
    public MovementData MovementData => _movementData;

    private const float SPEED_BLEND_DURATION = 0.25f;

    private string _speedTweenID;

    private void Awake()
    {
        Setup();
    }

    private void OnEnable()
    {
        EventManager.OnFeverModeEnabled.AddListener(OnFeverModeEnabled);
        EventManager.OnFeverModeDisabled.AddListener(OnFeverModeDisabled);
    }

    private void OnDisable()
    {
        EventManager.OnFeverModeEnabled.RemoveListener(OnFeverModeEnabled);
        EventManager.OnFeverModeDisabled.RemoveListener(OnFeverModeDisabled);
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

    private void OnFeverModeEnabled() 
    {
        SetSpeed(MovementData.FeverSpeed);
    }

    private void OnFeverModeDisabled() 
    {
        SetSpeed(MovementData.DefaultSpeed);
    }

    private void SetSpeed(float endValue)
    {
        DOTween.Kill(_speedTweenID);
        DOTween.To(() => CurrentSpeed, (x) => CurrentSpeed = x, endValue, SPEED_BLEND_DURATION).SetId(_speedTweenID).SetEase(Ease.Linear);
    }

    private void Setup() 
    {
        _speedTweenID = GetInstanceID() + "SpeedTweenID";
        CurrentSpeed = MovementData.DefaultSpeed;
    }
}
