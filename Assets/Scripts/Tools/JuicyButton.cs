using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class JuicyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button button;
    public GameObject scaledObject;
    private Tween scaleTween;
    public bool vibrate = true;
    public bool sound = true;
    bool canScale = true;

    private float transitionTime = 0.17f;
    public float scaleFactor = 0.9f;

    float timeCanUse = 0;

    Vector3 startScale;

    private void Awake()
    {
        if (!button)
            button = GetComponent<Button>();
        if (scaledObject == null)
            scaledObject = gameObject;

        startScale = scaledObject.transform.localScale;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (button != null && button.interactable && canScale && Time.time >= timeCanUse)
        {
            Cancel();
            scaleTween = scaledObject.transform.DOScale(startScale * scaleFactor, transitionTime).SetEase(Ease.OutBack).SetUpdate(true); canScale = false;
            if (vibrate)
                VibrationManager.VibrateMedium();
            if (sound)
                AudioManager.Instance.PlayButtonSound();
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (!canScale && scaledObject != null && Time.time >= timeCanUse)
        {
            Cancel();
            scaleTween = scaledObject.transform.DOScale(startScale, transitionTime).SetEase(Ease.OutBack).SetUpdate(true); canScale = true;
        }
    }

    public void Cancel(bool finishTween = false, float secondToLock = 0)
    {
        timeCanUse = Time.time + secondToLock;
        scaleTween.Kill(finishTween);
    }
}