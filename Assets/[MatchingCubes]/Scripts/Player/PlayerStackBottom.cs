using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackBottom : MonoBehaviour
{
    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;

    private void OnEnable()
    {
        EventManager.OnRampJumpingStarted.AddListener(OnJumpStarted);
        EventManager.OnRampJumpingCompleted.AddListener(OnJumpCompleted);
    }

    private void OnDisable()
    {
        EventManager.OnRampJumpingStarted.RemoveListener(OnJumpStarted);
        EventManager.OnRampJumpingCompleted.RemoveListener(OnJumpCompleted);
    }

    private void OnJumpStarted() 
    {
        if (PlayerStack.BottomCube == null)
            return;

        PlayerStack.BottomCube.Rigidbody.isKinematic = true;
    }

    private void OnJumpCompleted() 
    {
        if (PlayerStack.BottomCube == null)
            return;

        PlayerStack.BottomCube.Rigidbody.isKinematic = false;
    }
}
