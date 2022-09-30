using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinPanel : FadePanelBase
{
    public static CoinPanel Instance;

    [SerializeField] private Transform _coinIcon;  

    private const float FADE_DURATION = 0.25f;

    private const Ease MOVEMENT_EASE = Ease.Linear;
    private const float MOVEMENT_DURATION = 1f;

    private const Ease SCALE_EASE = Ease.Linear;
    private const float SCALE_DURATION = 0.2f;
    private const float MIN_SCALE_MULTIPLIER = 0.01f;

    private const float PUNCH_STRENGTH = 0.2f;
    private const float PUNCH_DURATION = 0.3f;
    private const Ease PUNCH_EASE = Ease.InOutSine;

    private const string COIN_POOL_ID = "Coin";

    private string _punchTweenID;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
        _punchTweenID = _coinIcon.GetInstanceID() + "PunchTweenID";
    }

    private void OnEnable()
    {
        EventManager.OnSceneLoaded.AddListener(() => ShowPanelWithFade(FADE_DURATION));
        EventManager.OnLevelFailed.AddListener(HidePanel);
        EventManager.OnLevelCompleted.AddListener(HidePanel);
        EventManager.OnSceneLoading.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        EventManager.OnSceneLoaded.RemoveListener(() => ShowPanelWithFade(FADE_DURATION));
        EventManager.OnLevelFailed.RemoveListener(HidePanel);
        EventManager.OnLevelCompleted.RemoveListener(HidePanel);
        EventManager.OnSceneLoading.RemoveListener(HidePanel);
    }

    public void CreateCoin(Vector3 worldPosition, float coinValue) 
    {
        Vector3 spawnPosition = Utilities.WorldToUISpace(UICanvas.Instance.Canvas, worldPosition);
        GameObject coin = PoolingSystem.Instance.InstantiateFromPool(COIN_POOL_ID, spawnPosition, Quaternion.identity);
        coin.transform.SetParent(_coinIcon);

        CoinMovement(coin, coinValue);
        CoinScale(coin);
    }

    private void CoinMovement(GameObject coin, float coinValue) 
    {
        string tweenID = coin.GetInstanceID() + "CoinMovementTween";
        Utilities.LocalMovementTween(coin.transform, Vector3.zero, MOVEMENT_DURATION, tweenID, MOVEMENT_EASE, onComplete: () => OnCoinMovementCompleted(coin, coinValue));
    }

    private void CoinScale(GameObject coin)
    {
        Vector3 defaultScale = coin.transform.localScale;
        coin.transform.localScale *= MIN_SCALE_MULTIPLIER;

        string tweenID = coin.GetInstanceID() + "CoinScaleTween";
        Utilities.ScaleTween(coin.transform, defaultScale, SCALE_DURATION, tweenID, SCALE_EASE);
    }

    private void PunchScaleTween()
    {
        DOTween.Kill(_punchTweenID);
        _coinIcon.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION, vibrato: 1).SetId(_punchTweenID).SetEase(PUNCH_EASE);
    }

    private void AddCoin(float coinValue)
    {
        GameManager.Instance.Coin += coinValue;
        EventManager.OnCoinDataChanged.Invoke();
    }

    private void OnCoinMovementCompleted(GameObject coin, float coinValue) 
    {
        AddCoin(coinValue);
        PunchScaleTween();
        PoolingSystem.Instance.DestroyPoolObject(coin);
    }
}
