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

    public static Vector3 WorldToUISpace(Canvas canvas, Vector3 worldPosition)
    {       
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out Vector2 localPoint);        
        return canvas.transform.TransformPoint(localPoint);
    }

    public static Vector3 BezierCurve(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        var ab = Vector3.Lerp(a, b, t);
        var bc = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(ab, bc, t);
    }
}
