using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;


    private void Start() {
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        DeliveryManager.Instance.OnDeliverySuccess += OnDeliverySuccess;
        gameObject.SetActive(false);
        SetScore(score);
    }

    private void OnDeliverySuccess(object sender, System.EventArgs e) {
        score++;
        SetScore(score);
    }

    private void OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            gameObject.SetActive(true);
        }
    }

    public void SetScore(int score) {
        this.scoreText.text = "Score: " + score;
    }
}
