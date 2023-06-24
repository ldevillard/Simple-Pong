using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class PaddleController : MonoBehaviour
{
    [SerializeField] SpriteRenderer render;

    protected float speed;

    protected float spriteSize;
    protected float camWidth;

    protected bool isReady;

    Vector2 startScale;

    void Awake()
    {
        GameManager.OnGameStarted += OnGameStarted;
    }

    void Start()
    {
        Init();
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted;
    }

    void Init()
    {
        speed = GameManager.Instance.gameData.PaddleSpeed;
        spriteSize = render.bounds.size.x;
        camWidth = CameraController.Instance.Cam.orthographicSize * 2 * CameraController.Instance.Cam.aspect;
        startScale = transform.localScale;

        transform.localScale = startScale * 0.01f;
        render.color = render.color.SetAlpha(0);
    }

    protected virtual void OnGameStarted()
    {
        isReady = true;

        render.DOFade(1, 0.3f);
        transform.DOScale(startScale, 0.3f).SetEase(Ease.OutBack);
    }

    void Update()
    {
        if (!isReady) return;

        Move();
    }

    protected abstract void Move();
}
