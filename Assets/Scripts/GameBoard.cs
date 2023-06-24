using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameBoard : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] wallsLR;
    [SerializeField] SpriteRenderer[] wallsTB;

    [SerializeField] SpriteRenderer[] Goals;

    void Awake()
    {
        GameManager.OnGameStarted += OnGameStarted;
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted;
    }

    void Start()
    {
        foreach (var item in Goals)
        {
            item.color = item.color.SetAlpha(0);
            item.gameObject.SetActive(false);
        }

        foreach (var item in wallsTB)
            item.gameObject.SetActive(true);
        foreach (var item in wallsLR)
            item.gameObject.SetActive(true);

        Camera cam = CameraController.Instance.Cam;

        float camWidth = cam.orthographicSize * 2 * cam.aspect;
        float camHeight = cam.orthographicSize * 2;

        float spriteWidth = wallsLR[0].bounds.size.x;
        float spriteHeight = wallsTB[0].bounds.size.y;

        // Positionnement des murs gauche/droite
        foreach (SpriteRenderer wall in wallsLR)
        {
            bool isLeft = wall.transform.position.x < 0;
            float newPos = isLeft ? -camWidth / 2 : camWidth / 2;
            newPos -= isLeft ? spriteWidth / 2 : -spriteWidth / 2;

            Vector2 pos = wall.transform.position;
            pos.x = newPos;
            wall.transform.position = pos;
        }

        // Positionnement des murs haut/bas
        foreach (SpriteRenderer wall in wallsTB)
        {
            bool isBottom = wall.transform.position.y < 0;
            float newPos = isBottom ? -camHeight / 2 : camHeight / 2;
            newPos -= isBottom ? spriteHeight / 2 : -spriteHeight / 2;

            Vector2 pos = wall.transform.position;
            pos.y = newPos;
            wall.transform.position = pos;
        }
    }

    void OnGameStarted()
    {
        foreach (var item in wallsTB)
            item.gameObject.SetActive(false);

        foreach (var item in Goals)
        {
            item.gameObject.SetActive(true);
            if (item.gameObject.CompareTag("Wall"))
            {
                Color.RGBToHSV(GameManager.Instance.gameData.BoardColor, out float h, out float s, out float v);
                if (v < 0.5f)
                    item.color = GameManager.Instance.gameData.BoardColor.LightColor(0.1f);
                else
                    item.color = GameManager.Instance.gameData.BoardColor.DarkColor(0.1f);

            }
            item.DOColor(item.color.SetAlpha(1), 0.5f);
        }
    }

}
