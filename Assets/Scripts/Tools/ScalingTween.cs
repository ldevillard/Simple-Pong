using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalingTween : MonoBehaviour
{
    public float scaleFactor;
    public float duration;
    public Ease ease = Ease.InOutSine;

    void Start()
    {
        Vector2 startScale = transform.localScale;
        transform.DOScale(startScale * scaleFactor, duration).From(startScale).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
    }
}
