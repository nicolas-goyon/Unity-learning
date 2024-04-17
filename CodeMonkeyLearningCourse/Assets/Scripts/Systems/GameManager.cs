using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState {
        WaitingToStart,
        CountDownToStart,
        Playing,
        GameOver
    }

    private GameState gameState = GameState.WaitingToStart;

    [SerializeField] private float waitToStartCountDown = 1f;
    private float waitToStartTimer;


    [SerializeField] private float countDownToStartCountDown = 3f;
    private float countDownToStartTimer;

    [SerializeField] private float timeToPlay = 10f;
    private float timeToPlayTimer;


    // ***** EVENTS *****

    public event EventHandler OnGameStateChanged;






    // ***** END EVENTS *****


    public static GameManager Instance { get; private set; }

    private void Awake() { 
           if (Instance == null) {
            Instance = this;
        } else {
            throw new System.Exception("There can only be one GameManager.");
        }
    }

    private void Start() {
        waitToStartTimer = waitToStartCountDown;
    }


    private void Update() {
        switch (gameState) {
            case GameState.WaitingToStart:
                WaitingToStart();
                break;
            case GameState.CountDownToStart:
                CountDownToStart();
                break;
            case GameState.Playing:
                Playing();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

    private void WaitingToStart() {
        waitToStartTimer -= Time.deltaTime;
        if (waitToStartTimer <= 0) {
            ChangeGameState(GameState.CountDownToStart);
            countDownToStartTimer = countDownToStartCountDown;
        }
    }

    private void CountDownToStart() {
        countDownToStartTimer -= Time.deltaTime;
        if (countDownToStartTimer <= 0) {
            ChangeGameState(GameState.Playing);
            timeToPlayTimer = timeToPlay;
        }
    }

    private void Playing() {
        timeToPlayTimer -= Time.deltaTime;
        if (timeToPlayTimer <= 0) {
            ChangeGameState(GameState.GameOver);

        }
    }


    private void GameOver() {

    }

    public bool IsGamePlaying() {
        return gameState == GameState.Playing;
    }

    public bool IsCountDownToStart() {
        return gameState == GameState.CountDownToStart;
    }


    public bool IsGameOver() {
        return gameState == GameState.GameOver;
    }

    private void ChangeGameState(GameState newGameState) {
        gameState = newGameState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public float GetCountDownToStartTimer() {
        return countDownToStartTimer;
    }
}
