using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    private TrailRenderer TrailRenderer => _trailRenderer == null ? _trailRenderer = GetComponentInChildren<TrailRenderer>() : _trailRenderer;

    private const float DESTROY_DELAY = 3f;

    private Coroutine _destroyCoroutine = null;

    public void Initialize(Color trailColor) 
    {
        if (_destroyCoroutine != null)
            StopCoroutine(_destroyCoroutine);

        SetTrailColor(trailColor);

        TrailRenderer.Clear();
        TrailRenderer.emitting = true;
    }

    public void DestroyTrail() 
    {
        TrailRenderer.emitting = false;

        if (_destroyCoroutine != null)
            StopCoroutine(_destroyCoroutine);

        _destroyCoroutine = Utilities.ExecuteAfterDelay(this, DESTROY_DELAY, () => PoolingSystem.Instance.DestroyPoolObject(gameObject));
    }

    private void SetTrailColor(Color trailColor) 
    {
        TrailRenderer.startColor = trailColor;
        TrailRenderer.endColor = trailColor;
    }
}
