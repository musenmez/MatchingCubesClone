using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtraGravity : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInChildren<Rigidbody>() : _rigidbody;

    private const float EXTRA_GRAVITY = 25f;

    private void FixedUpdate()
    {
        ApplyExtraGravity();
    }

    private void ApplyExtraGravity()
    {
        if (Rigidbody.isKinematic)
            return;

        Rigidbody.AddForce(Vector3.down * EXTRA_GRAVITY);
    }
}
