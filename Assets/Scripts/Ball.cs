using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public GameObject pivot;
    public Rigidbody2D rb;
    public Collider2D cl;
    float speed;

    public TrailRenderer trail;
    public SpriteRenderer spriteRenderer;

    Tween scalingTween;

    Vector2 startScale;
    Vector2 startPivotScale;

    bool isReady;

    [SerializeField]
    ParticleSystem explosionFx;
    [SerializeField]
    ParticleSystem powerUpFx;

    public AudioData audioData;

    Coroutine IncreaseSpeedCoroutine;

    void Awake()
    {
        startPivotScale = pivot.transform.localScale;
        pivot.transform.localScale = Vector2.zero;
    }

    public void Init(float speed)
    {
        startScale = transform.localScale;
        this.speed = speed;

        Vector2 dir = RandomDirection();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(pivot.transform.DOScale(startPivotScale, 0.3f).SetEase(Ease.OutSine))
        .Join(DOTween.To(() => transform.eulerAngles, x => transform.eulerAngles = x, new Vector3(0, 0, angle), 0.3f).SetEase(Ease.OutSine));

        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() =>
        {
            AudioManager.Instance.PlaySound(audioData.GetClip(1));
            isReady = true;
            pivot.transform.localScale = Vector2.zero;
            rb.velocity = dir * speed;

            if (GameManager.Instance.gameData.ballSpeedIncreaseFactor > 1)
                IncreaseSpeedCoroutine = StartCoroutine(IncreaseSpeed(GameManager.Instance.gameData.ballSpeedIncreaseFactor));
        });
    }

    public void InitMenu(float _speed)
    {
        startScale = transform.localScale;
        speed = _speed;
        rb.velocity = RandomDirection() * _speed;
    }

    Vector2 RandomDirection()
    {
        float x = (Random.value > 0.5f) ? 1f : -1f;
        float y = (Random.value > 0.5f) ? 1f : -1f;

        return new Vector2(x, y).normalized;
    }

    IEnumerator IncreaseSpeed(float factor)
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
            spriteRenderer.DOColor(Color.red, 0.2f).OnComplete(() => spriteRenderer.DOColor(Color.white, 0.2f));
            speed *= factor;
            AudioManager.Instance.PlaySound(audioData.GetClip(2));
            powerUpFx.Play();
        }
    }

    public void Explode()
    {
        Instantiate(explosionFx, transform.position, Quaternion.identity);
        if (IncreaseSpeedCoroutine != null)
            StopCoroutine(IncreaseSpeedCoroutine);
        Destroy(gameObject);
    }

    void Update()
    {
        if (!isReady) return;

        if (Mathf.Abs(rb.velocity.x) < speed / 4)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed, rb.velocity.y);
        if (Mathf.Abs(rb.velocity.y) < speed / 4)
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * speed);

        rb.velocity = speed * (rb.velocity.normalized);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (scalingTween != null)
            scalingTween.Kill(true);
        scalingTween = transform.DOPunchScale(Vector3.one * 0.1f, 0.2f)
        .OnComplete(() => transform.localScale = startScale);

        AudioManager.Instance.PlaySound(audioData.GetClip(0), true);
        VibrationManager.VibrateLight();
    }
}
