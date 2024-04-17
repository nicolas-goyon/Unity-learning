using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image clockImage;

    private void Start() {
        GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        gameObject.SetActive(false);
    }

    private void Instance_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGamePlaying()) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    private void Update() {
        float amount = GameManager.Instance.GetTimeToPlayTimer() / GameManager.Instance.GetTimeToPlay();
        clockImage.fillAmount = amount;
    }
}
