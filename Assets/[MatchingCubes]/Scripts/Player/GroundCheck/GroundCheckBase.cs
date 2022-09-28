using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GroundCheckBase : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public abstract Transform OriginBody { get; protected set; }

    [SerializeField] private LayerMask _groundLayer;

    protected const float ORIGIN_HEIGHT_OFFSET = 5f;
    protected const float RAYCAST_DISTANCE = 5.25f;

    [HideInInspector]
    public UnityEvent OnGroundedStatusChanged = new UnityEvent();

    protected virtual void FixedUpdate()
    {
        CheckGround();
    }

    protected void CheckGround() 
    {
        if (OriginBody == null) 
        {
            SetGroundedStatus(false);
            return;
        }

        Vector3 rayOrigin = OriginBody.position + Vector3.up * ORIGIN_HEIGHT_OFFSET;
        if (Physics.Raycast(rayOrigin, Vector3.down, RAYCAST_DISTANCE, _groundLayer))
        {
            SetGroundedStatus(true);
        }

        else
        {
            SetGroundedStatus(false);
        }
    }

    private void SetGroundedStatus(bool isGrounded) 
    {
        if (IsGrounded == isGrounded)
            return;

        IsGrounded = isGrounded;
        OnGroundedStatusChanged.Invoke();
    }    
}
