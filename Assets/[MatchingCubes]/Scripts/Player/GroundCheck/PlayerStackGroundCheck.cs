using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackGroundCheck : GroundCheckBase
{
    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;

    public override Transform OriginBody { get; protected set; }

    private void OnEnable()
    {
        PlayerStack.OnStackUpdated.AddListener(SetOriginBody);
    }

    private void OnDisable()
    {
        PlayerStack.OnStackUpdated.RemoveListener(SetOriginBody);
    }

    private void SetOriginBody() 
    {
        OriginBody = PlayerStack.BottomCube != null ? PlayerStack.BottomCube.transform : null;
    }
}
