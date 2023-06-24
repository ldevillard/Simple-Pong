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
    static public event Action OnGameEnded;

    public enum GameState
    {
        Menu,
        InGame,
        GameOver
    }

    public GameState CurrentState;

    public GameData gameData;

    public Ball ballPrefab;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CurrentState = GameState.Menu;
    }

    public void StartGame()
    {
        CurrentState = GameState.InGame;
        OnGameStarted?.Invoke();
        StartCoroutine(PlayTurn());
    }

    public void EndGame()
    {
        CurrentState = GameState.GameOver;
        OnGameEnded?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    IEnumerator PlayTurn()
    {
        yield return new WaitForSeconds(0.5f);
        Ball ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        Vector2 ballScale = ball.transform.localScale;
        ball.transform.DOScale(ballScale, 0.3f).From(0).SetEase(Ease.OutBack).SetDelay(0.3f);
        yield return new WaitForSeconds(1f);
        ball.Init(gameData.BallSpeed);
    }
}
