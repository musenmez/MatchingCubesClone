using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition() 
    {
        if (Player.Instance == null)
            return;

        Vector3 targetPosition = transform.position;
        targetPosition.z = Player.Instance.transform.position.z;

        transform.position = targetPosition;
    }
}
