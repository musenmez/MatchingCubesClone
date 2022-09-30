using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    private TrailRenderer TrailRenderer => _trailRenderer == null ? _trailRenderer = GetComponentInChildren<TrailRenderer>() : _trailRenderer;

    public void Initialize(Color trailColor) 
    {
        SetTrailColor(trailColor);
        TrailRenderer.emitting = true;
    }

    public void DisableTrail() 
    {
        TrailRenderer.emitting = false;
    }

    private void SetTrailColor(Color trailColor) 
    {
        TrailRenderer.startColor = trailColor;
        TrailRenderer.endColor = trailColor;
    }
}
