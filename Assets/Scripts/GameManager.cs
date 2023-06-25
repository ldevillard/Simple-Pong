using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    static public event Action OnGameStarted;
    static public event Action<bool> OnGameEnded;

    public enum GameState
    {
        Menu,
        InGame,
        Pause,
        GameOver
    }

    public GameState CurrentState;

    public GameData gameData;

    public Goal[] goals;
    public Ball ballPrefab;
    public Ball currentBall;

    Ball menuBall;

    public GameObject[] ObstaclePrefab;
    public SpriteRenderer GenerationArea;

    public AudioData audioData;

    void Awake()
    {
        Instance = this;

        Application.targetFrameRate = 60;
    }

    void Start()
    {
        CurrentState = GameState.Menu;

        foreach (var item in goals)
            item.OnGoalReached += OnGoalReached;

        Ball ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        menuBall = ball;
        menuBall.InitMenu(gameData.BallSpeed);
    }

    void OnDestroy()
    {
        foreach (var item in goals)
            item.OnGoalReached -= OnGoalReached;
    }

    public void StartGame()
    {
        CurrentState = GameState.InGame;
        OnGameStarted?.Invoke();
        StartCoroutine(PlayTurn());
        StartCoroutine(RandomObstacleGenerator());

        VibrationManager.VibrateSelection();

        menuBall.cl.enabled = false;
        menuBall.trail.transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
        menuBall.transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() => Destroy(menuBall.gameObject));
    }

    public void EndGame()
    {
        CurrentState = GameState.GameOver;
        OnGameEnded?.Invoke(ScoreManager.Instance.PlayerWin());
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    void OnGoalReached(Goal goal)
    {
        CurrentState = GameState.Pause;

        currentBall.Explode();
        currentBall = null;

        CameraController.Instance.Shake(0.3f, 0.3f);

        if (goal.isPlayer)
            VibrationManager.VibrateFailure();
        else
            VibrationManager.VibrateSuccess();

        AudioManager.Instance.PlaySound(audioData.GetClip(0));

        if (ScoreManager.Instance.AddScore(!goal.isPlayer) != 0)
            EndGame();
        else
            StartCoroutine(PlayTurn());
    }

    IEnumerator PlayTurn()
    {
        yield return new WaitForSeconds(0.5f);

        Ball ball = Instantiate(ballPrefab, Vector3.zero.SetZ(-5), Quaternion.identity);
        currentBall = ball;
        Vector2 ballScale = ball.transform.localScale;
        AudioManager.Instance.PlaySound(audioData.GetClip(1), true);
        ball.transform.DOScale(ballScale, 0.3f).From(0).SetEase(Ease.OutBack).SetDelay(0.3f);
        yield return new WaitForSeconds(1f);
        CurrentState = GameState.InGame;

        ball.Init(gameData.BallSpeed);
    }

    IEnumerator RandomObstacleGenerator()
    {
        while (true)
        {
            if (CurrentState == GameState.InGame)
            {
                // if (CurrentState != GameState.Pause)
                // {
                yield return new WaitForSeconds(15f);

                float randomRotation = UnityEngine.Random.Range(0f, 360f);
                GameObject obstacle = Instantiate(ObstaclePrefab[UnityEngine.Random.Range(0, ObstaclePrefab.Length)], GetRandomPositionInBounds(GenerationArea), Quaternion.Euler(0f, 0f, randomRotation));
                Vector2 obstacleScale = obstacle.transform.localScale;

                AudioManager.Instance.PlaySound(audioData.GetClip(1), true);

                obstacle.transform.DOScale(obstacleScale, 0.3f).From(0).SetEase(Ease.OutBack);
                // for (int i = 0; i < 10; i++)
                // {
                //     if (CurrentState == GameState.Pause)
                //         break;
                //     yield return new WaitForSeconds(0.5f);
                // }
                yield return new WaitForSeconds(5f);
                AudioManager.Instance.PlaySound(audioData.GetClip(2), true);
                obstacle.transform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() => Destroy(obstacle));
                // }
            }
            else if (CurrentState == GameState.GameOver)
            {
                yield break;
            }
            else
            {
                yield return null;
            }
        }
    }

    Vector3 GetRandomPositionInBounds(SpriteRenderer spriteRenderer)
    {
        Vector3 min = spriteRenderer.bounds.min;
        Vector3 max = spriteRenderer.bounds.max;

        float x = UnityEngine.Random.Range(min.x, max.x);
        float y = UnityEngine.Random.Range(min.y, max.y);

        return new Vector3(x, y, 0f);
    }
}
