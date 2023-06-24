using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static public UIController Instance;

    public GameObject backgroundButton;

    public CanvasGroup MenuPanel;
    public CanvasGroup GamePanel;
    public CanvasGroup GameOverPanel;

    public Text PlayerScoreText;
    public Text AIScoreText;

    public GameObject RestartButton;

    public GameOverPanel WinPanel;
    public GameOverPanel LosePanel;

    public AudioData audioData;

    void Awake()
    {
        Instance = this;

        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameEnded += OnGameEnded;
    }

    void Start()
    {
        MenuPanel.alpha = 1;
        GamePanel.alpha = 0;
        GameOverPanel.alpha = 0;

        GamePanel.interactable = false;
        GameOverPanel.interactable = false;
        GamePanel.blocksRaycasts = false;
        GameOverPanel.blocksRaycasts = false;

        RestartButton.transform.localScale = Vector3.zero;
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameEnded -= OnGameEnded;
    }

    void OnGameStarted()
    {
        MenuPanel.DOFade(0, 0.3f);
        GamePanel.DOFade(1, 0.3f);

        GamePanel.interactable = true;
        MenuPanel.interactable = false;
        GamePanel.blocksRaycasts = true;
        MenuPanel.blocksRaycasts = false;
    }

    public void TriggerStartGame()
    {
        GameManager.Instance.StartGame();
        backgroundButton.SetActive(false);
    }

    public void UpdateScoreText(int socre, bool player)
    {
        if (player)
        {
            PlayerScoreText.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f);
            PlayerScoreText.text = socre.ToString();
        }
        else
        {
            AIScoreText.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f);
            AIScoreText.text = socre.ToString();
        }
    }

    void OnGameEnded(bool player)
    {
        GameOverPanel.blocksRaycasts = true;
        GameOverPanel.DOFade(1, 1f).OnComplete(() => GameOverPanel.interactable = true);

        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(1);

        if (player)
            sequence = WinPanel.Display(sequence);
        else
            sequence = LosePanel.Display(sequence);

        sequence.Append(RestartButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack));
        sequence.AppendCallback(() =>
        {
            if (player)
            {
                VibrationManager.VibrateSuccess();
                AudioManager.Instance.PlaySound(audioData.GetClip(0));
            }
            else
            {
                VibrationManager.VibrateFailure();
                AudioManager.Instance.PlaySound(audioData.GetClip(1));
            }
        });
    }
}
