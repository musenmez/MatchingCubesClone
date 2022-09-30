using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndVirtualCamera : VirtualCameraBase
{
    private const float BLEND_DURATION = 0.5f;

    protected override void OnEnable()
    {
        base.OnEnable();

        EventManager.OnLevelFailed.AddListener(ActivateCamera);
        EventManager.OnLevelCompleted.AddListener(ActivateCamera);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        EventManager.OnLevelFailed.RemoveListener(ActivateCamera);
        EventManager.OnLevelCompleted.RemoveListener(ActivateCamera);
    }

    private void ActivateCamera() 
    {
        CameraManager.Instance.ActivateCamera(this, BLEND_DURATION);
    }
}
