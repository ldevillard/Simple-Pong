using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Goal : MonoBehaviour
{
    public event Action<Goal> OnGoalReached;

    public bool isPlayer;

    void Start()
    {
        transform.localScale = transform.localScale.SetY(transform.localScale.y * GameManager.Instance.gameData.GoalsWidth);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.InGame || !other.gameObject.CompareTag("Ball"))
            return;
        OnGoalReached?.Invoke(this);
    }
}
