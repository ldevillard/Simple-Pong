using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    float speed;

    public void Init(float speed)
    {
        this.speed = speed;
        rb.velocity = RandomDirection() * speed;
    }

    Vector2 RandomDirection()
    {
        float x = (Random.value > 0.5f) ? 1f : -1f;
        float y = (Random.value > 0.5f) ? 1f : -1f;

        return new Vector2(x, y).normalized;
    }
}
