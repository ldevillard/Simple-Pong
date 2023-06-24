using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverPanel : MonoBehaviour
{
    public GameObject title;
    public GameObject emoji;

    public AudioData audioData;

    void Start()
    {
        title.transform.localScale = Vector3.zero;
        emoji.transform.localScale = Vector3.zero;
    }

    public Sequence Display(Sequence sequence)
    {

        sequence.AppendCallback(() =>
        {
            AudioManager.Instance.PlaySound(audioData.GetClip(0), true);
        });
        sequence.Append(title.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));

        sequence.AppendCallback(() =>
        {
            AudioManager.Instance.PlaySound(audioData.GetClip(0), true);
        });
        sequence.Append(emoji.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));

        return sequence;
    }
}
