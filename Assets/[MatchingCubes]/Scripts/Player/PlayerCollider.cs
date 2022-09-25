using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private PlayerStack _playerStack;
    private PlayerStack PlayerStack => _playerStack == null ? _playerStack = GetComponentInParent<PlayerStack>() : _playerStack;

    [SerializeField] private List<BoxCollider> _colliders = new List<BoxCollider>();

    private const float OFFSET = 2f;

    private void Awake()
    {        
        SetColliderSize();
    }

    private void OnEnable()
    {
        PlayerStack.OnStackUpdated.AddListener(SetColliderSize);
    }

    private void OnDisable()
    {
        PlayerStack.OnStackUpdated.RemoveListener(SetColliderSize);
    }

    private void SetColliderSize() 
    {
        foreach (var collider in _colliders)
        {
            Vector3 size = collider.size;
            size.y = PlayerStack.Cubes.Count + OFFSET;            
            collider.size = size;

            float centerY = OFFSET - size.y / 2f;
            collider.center = Vector3.up * centerY;
        }
    }
}
