using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVIsual : MonoBehaviour{

    [SerializeField] private BaseCounter BaseCounter;

    [SerializeField] private GameObject[] selectedCounterVisual;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        foreach (GameObject obj in selectedCounterVisual) {
            obj.SetActive(e.selectedCounter == BaseCounter);
        }
    }

}
