using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private ParticleSystem ParticleSystem => _particleSystem == null ? _particleSystem = GetComponentInChildren<ParticleSystem>() : _particleSystem;

    private void OnEnable()
    {
        EventManager.OnFeverModeEnabled.AddListener(ActivateEffect);
        EventManager.OnFeverModeDisabled.AddListener(DisableEffect);
    }

    private void OnDisable()
    {
        EventManager.OnFeverModeEnabled.RemoveListener(ActivateEffect);
        EventManager.OnFeverModeDisabled.RemoveListener(DisableEffect);
    }

    private void ActivateEffect() 
    {
        SetEmission(true);
    }

    private void DisableEffect() 
    {
        SetEmission(false);
    }

    private void SetEmission(bool enabled) 
    {
        var emission = ParticleSystem.emission;
        emission.enabled = enabled;
    }
}
