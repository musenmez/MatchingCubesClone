using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class Utilities
{   
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T value = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = value;
        }
    }

    public static void LocalMovementTween(Transform target, Vector3 localPosition, float duration, string tweenID, Ease tweenEase)
    {
        DOTween.Kill(tweenID);
        target.DOLocalMove(localPosition, duration).SetId(tweenID).SetEase(tweenEase);
    }
}
