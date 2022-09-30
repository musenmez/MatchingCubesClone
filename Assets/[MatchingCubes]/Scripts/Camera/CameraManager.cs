using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    private List<VirtualCameraBase> _virtualCameras = new List<VirtualCameraBase>();
    public List<VirtualCameraBase> VirtualCameras { get => _virtualCameras; private set => _virtualCameras = value; }  
    public VirtualCameraBase CurrentActiveCamera { get; private set; }

    private void OnEnable()
    {
        EventManager.OnSceneLoading.AddListener(ResetList);
    }

    private void OnDisable()
    {
        EventManager.OnSceneLoading.RemoveListener(ResetList);
    }

    public void AddCamera(VirtualCameraBase virtualCamera)
    {
        if (!VirtualCameras.Contains(virtualCamera))
        {
            VirtualCameras.Add(virtualCamera);
        }
    }

    public void RemoveCamera(VirtualCameraBase virtualCamera)
    {
        if (VirtualCameras.Contains(virtualCamera))
        {
            VirtualCameras.Remove(virtualCamera);
        }
    }

    public void ActivateCamera(VirtualCameraBase virtualCamera, float blendTime)
    {
        if (CurrentActiveCamera == virtualCamera)
            return;

        CameraBrain.Instance.CinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, blendTime);
        virtualCamera.VirtualCamera.Priority = 20;
        for (int i = 0; i < VirtualCameras.Count; i++)
        {
            if (VirtualCameras[i] == virtualCamera) continue;
            VirtualCameras[i].VirtualCamera.Priority = 10;
        }

        CurrentActiveCamera = virtualCamera;
    }
    
    private void ResetList()
    {
        VirtualCameras.Clear();
    }
}
