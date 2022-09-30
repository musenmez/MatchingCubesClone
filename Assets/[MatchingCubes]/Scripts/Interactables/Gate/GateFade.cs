using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GateFade : MonoBehaviour
{
    private InteractableBase _interactableBase;
    private InteractableBase InteractableBase => _interactableBase == null ? _interactableBase = GetComponentInParent<InteractableBase>() : _interactableBase;

    private SpriteRenderer[] _spriteRenderers;
    private SpriteRenderer[] SpriteRenderers => _spriteRenderers == null ? _spriteRenderers = GetComponentsInChildren<SpriteRenderer>() : _spriteRenderers;

    private MeshRenderer[] _meshRenderers;
    private MeshRenderer[] MeshRenderers => _meshRenderers == null ? _meshRenderers = _meshRendererParent.GetComponentsInChildren<MeshRenderer>() : _meshRenderers;

    private TextMeshPro[] _textMeshes;
    private TextMeshPro[] TextMeshes => _textMeshes == null ? _textMeshes = GetComponentsInChildren<TextMeshPro>() : _textMeshes;

    public bool IsFadeMaterialSet { get; private set; }

    [SerializeField] private Transform _meshRendererParent;
    [SerializeField] private Material _baseFadeMaterial;

    private const float MIN_ALPHA = 0;
    private const float FADE_DURATION = 0.2f;
    private const Ease FADE_EASE = Ease.Linear;

    private string _fadeTweenID;
    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
        _fadeTweenID = GetInstanceID() + "FadeTweenID";
    }

    private void OnEnable()
    {
        InteractableBase.OnInteracted.AddListener(FadeOut);
    }

    private void OnDisable()
    {
        InteractableBase.OnInteracted.RemoveListener(FadeOut);
    }

    private void FadeOut() 
    {
        DOTween.Kill(_fadeTweenID);
        FadeSpriteRenderers();
        FadeTextMeshes();
        FadeMeshRenderers();
    }

    private void FadeSpriteRenderers() 
    {
        foreach (var spriteRenderer in SpriteRenderers)
        {
            spriteRenderer.DOFade(MIN_ALPHA, FADE_DURATION).SetId(_fadeTweenID).SetEase(FADE_EASE);
        }
    }

    private void FadeTextMeshes() 
    {
        foreach (var textMesh in TextMeshes)
        {
            textMesh.DOFade(MIN_ALPHA, FADE_DURATION).SetId(_fadeTweenID).SetEase(FADE_EASE);
        }
    }

    private void FadeMeshRenderers() 
    {       
        SetFadeMaterials();

        float currentAlpha = 1;
        DOTween.To(() => currentAlpha, (x) => currentAlpha = x, MIN_ALPHA, FADE_DURATION).SetId(_fadeTweenID).SetEase(FADE_EASE).OnUpdate(() =>
        {
            foreach (var meshRenderer in MeshRenderers)
            {
                meshRenderer.GetPropertyBlock(_propertyBlock);

                Color color = _propertyBlock.GetColor("_Color");
                color.a = currentAlpha;

                _propertyBlock.SetColor("_Color", color);
                meshRenderer.SetPropertyBlock(_propertyBlock);
            }
        });       
    }

    private void SetFadeMaterials() 
    {
        if (IsFadeMaterialSet)
            return;

        IsFadeMaterialSet = true;
        foreach (var meshRenderer in MeshRenderers)
        {
            Material fadeMaterial = new Material(_baseFadeMaterial);
            fadeMaterial.SetColor("_Color", meshRenderer.material.GetColor("_Color"));
            fadeMaterial.SetTexture("_MainTex", meshRenderer.material.GetTexture("_MainTex"));
            meshRenderer.material = fadeMaterial;
        }
    }
}
