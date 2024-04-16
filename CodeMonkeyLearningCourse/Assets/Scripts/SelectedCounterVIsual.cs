using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVIsual : MonoBehaviour{

    [SerializeField] private ClearCounter ClearCounter;

    [SerializeField] private GameObject selectedCounterVisual;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        selectedCounterVisual.SetActive(e.selectedCounter == ClearCounter);
    }

}
