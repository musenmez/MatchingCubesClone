using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GroundCheckBase : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public virtual float DistanceOffset => 0.35f;
    public abstract Transform OriginBody { get; protected set; }

    [SerializeField] private LayerMask _groundLayer;

    protected const float ORIGIN_HEIGHT_OFFSET = 5f;
    protected const float SPHERECAST_RADIUS = 0.2f;

    private float _rayDistance;

    [HideInInspector]
    public UnityEvent OnGroundedStatusChanged = new UnityEvent();

    protected virtual void Awake() 
    {
        _rayDistance = ORIGIN_HEIGHT_OFFSET + DistanceOffset;
    }

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
        if (Physics.SphereCast(rayOrigin, SPHERECAST_RADIUS, Vector3.down, out RaycastHit hit, _rayDistance, _groundLayer))
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
