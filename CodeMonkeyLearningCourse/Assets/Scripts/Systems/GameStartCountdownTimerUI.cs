using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        gameObject.SetActive(false);
    }

    private void Update() {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownToStartTimer()).ToString("F0");
    }


    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsCountDownToStart()) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

}
