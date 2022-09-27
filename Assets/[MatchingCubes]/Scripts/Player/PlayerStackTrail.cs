using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackTrail : MonoBehaviour
{
    private PlayerStackGroundCheck _groundCheck;
    private PlayerStackGroundCheck GroundCheck => _groundCheck == null ? _groundCheck = GetComponentInParent<PlayerStackGroundCheck>() : _groundCheck;

    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;   
    public Trail CurrentTrail { get; private set; }

    [SerializeField] private Transform _trailParent;
    [SerializeField] private GameObject _trailPrefab;

    private void OnEnable()
    {
        GroundCheck.OnGroundedStatusChanged.AddListener(CheckTrail);
        PlayerStack.OnLastCubeTypeChanged.AddListener(CheckTrail);
    }

    private void OnDisable()
    {
        GroundCheck.OnGroundedStatusChanged.RemoveListener(CheckTrail);
        PlayerStack.OnLastCubeTypeChanged.RemoveListener(CheckTrail);
    }

    private void CheckTrail() 
    {
        if (GroundCheck.IsGrounded && PlayerStack.LastCubeType != CubeType.None)
            CreateTrail();
        else
            DisableCurrentTrail();
    }

    private void CreateTrail() 
    {
        CubeData cubeData = CubeDataManager.Instance.GetCubeData(PlayerStack.LastCubeType);
        if (cubeData == null)
            return;

        DisableCurrentTrail();

        Trail trail = Instantiate(_trailPrefab, _trailParent.position, Quaternion.identity, _trailParent).GetComponentInChildren<Trail>();
        trail.Initialize(cubeData.TrailColor);

        CurrentTrail = trail;
    }

    private void DisableCurrentTrail() 
    {
        if (CurrentTrail == null)
            return;

        CurrentTrail.DisableTrail();
    } 
}
