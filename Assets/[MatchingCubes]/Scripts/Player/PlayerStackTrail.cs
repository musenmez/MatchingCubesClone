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

    private const string TRAIL_POOL_ID = "Trail";

    [SerializeField] private Transform _trailParent;    

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

        Trail trail = PoolingSystem.Instance.InstantiateFromPool(TRAIL_POOL_ID, _trailParent.position, Quaternion.identity).GetComponentInChildren<Trail>();
        trail.transform.SetParent(_trailParent);
        trail.Initialize(cubeData.TrailColor);

        CurrentTrail = trail;
    }

    private void DisableCurrentTrail() 
    {
        if (CurrentTrail == null)
            return;

        CurrentTrail.DestroyTrail();
    } 
}
