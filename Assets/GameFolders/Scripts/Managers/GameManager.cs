using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlaying;

    public event Action OnGameStart;
    public event Action OnGameEnd;
    private void Awake()
    {
        instance = this;
    }
}
