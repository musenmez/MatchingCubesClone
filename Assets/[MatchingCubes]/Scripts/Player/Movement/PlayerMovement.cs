using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float CurrentSpeed { get; private set; }
    
    [SerializeField] private MovementData _movementData;
    public MovementData MovementData => _movementData;

    private void Update()
    {
        
    }
}
