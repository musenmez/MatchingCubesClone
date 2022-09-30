using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraBase : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    public CinemachineVirtualCamera VirtualCamera => _virtualCamera == null ? _virtualCamera = GetComponent<CinemachineVirtualCamera>() : _virtualCamera;

    protected virtual void OnEnable()
    {
        if (Managers.Instance == null) 
            return;

        CameraManager.Instance.AddCamera(this);
    }

    protected virtual void OnDisable()
    {
        if (Managers.Instance == null) 
            return;

        CameraManager.Instance.RemoveCamera(this);
    }

    public virtual void ActivateCamera(float blendTime)
    {
        CameraManager.Instance.ActivateCamera(this, blendTime);
    }
}
