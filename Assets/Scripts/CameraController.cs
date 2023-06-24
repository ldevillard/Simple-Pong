using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    static public CameraController Instance;

    public Camera Cam;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cam.backgroundColor = GameManager.Instance.gameData.BoardColor;
    }

    public void Shake(float duration, float strength = 3)
    {
        Cam.DOShakePosition(duration, strength);
    }
}
