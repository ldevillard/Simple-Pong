using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
