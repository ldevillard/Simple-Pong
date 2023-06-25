using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    //Mandatory part
    public float BallSpeed;
    public float GoalsWidth;
    public Color BoardColor;

    //Optional part
    public float PaddleSpeed;
    public float ballSpeedIncreaseFactor = 1;
}
