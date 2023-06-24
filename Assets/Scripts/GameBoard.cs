using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] walls;

    void Start()
    {
        Camera cam = CameraController.Instance.Cam;

        bool isLeft = false;

        float camWidth = cam.orthographicSize * 2 * cam.aspect;

        float spriteSize = walls[0].bounds.size.x;
        float newPos = isLeft ? -camWidth / 2 : camWidth / 2;
        newPos -= isLeft ? spriteSize / 2 : -spriteSize / 2;

        foreach (SpriteRenderer wall in walls)
        {
            Vector2 pos = wall.transform.position.SetX(newPos);
            wall.transform.position = pos;

            isLeft = !isLeft;
            newPos = isLeft ? -camWidth / 2 : camWidth / 2;
            newPos -= isLeft ? spriteSize / 2 : -spriteSize / 2;
        }
    }

}
