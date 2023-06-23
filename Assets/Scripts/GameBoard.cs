using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] Camera cam;

    void Start()
    {
        float camWidth = cam.orthographicSize * 2 * cam.aspect;


    }

}
