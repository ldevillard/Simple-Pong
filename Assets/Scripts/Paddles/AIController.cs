using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PaddleController
{
    float tempPosX;

    protected override void Move()
    {
        if (GameManager.Instance.currentBall != null)
        {
            Vector2 ballPos = GameManager.Instance.currentBall.transform.position;

            if (ballPos.y > 0)
            {
                Vector2 clampedPos = new Vector2(Mathf.Clamp(ballPos.x, -camWidth / 2 + spriteSize / 2, camWidth / 2 - spriteSize / 2), transform.position.y);
                transform.position = Vector2.Lerp(transform.position, clampedPos, (speed / 3) * Time.deltaTime);
            }
            else
            {
                if (Mathf.Abs(tempPosX - transform.position.x) < 0.1f)
                    tempPosX = Random.Range(-camWidth / 2 + spriteSize / 2, camWidth / 2 - spriteSize / 2);
                transform.position = Vector2.Lerp(transform.position, new Vector2(tempPosX, transform.position.y), (speed) / 10 * Time.deltaTime);
            }
        }
    }
}
