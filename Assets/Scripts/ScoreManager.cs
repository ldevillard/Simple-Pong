using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static public ScoreManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public int PlayerScore;
    public int AIScore;

    public int AddScore(bool player)
    {
        int updateScore;
        if (player)
        {
            PlayerScore++;
            updateScore = PlayerScore;
        }
        else
        {
            AIScore++;
            updateScore = AIScore;
        }

        UIController.Instance.UpdateScoreText(updateScore, player);

        if (PlayerScore == 3 || AIScore == 3)
            return 1;

        return 0;
    }

    public bool PlayerWin()
    {
        return PlayerScore > AIScore;
    }
}
