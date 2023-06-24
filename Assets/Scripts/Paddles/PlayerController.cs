using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : PaddleController
{
    protected override void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;

            Vector2 worldPos = CameraController.Instance.Cam.ScreenToWorldPoint(mousePos);

            float clampedX = Mathf.Clamp(worldPos.x, -camWidth / 2 + spriteSize / 2, camWidth / 2 - spriteSize / 2);

            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, transform.position.y), speed * Time.deltaTime);
        }
    }
}
