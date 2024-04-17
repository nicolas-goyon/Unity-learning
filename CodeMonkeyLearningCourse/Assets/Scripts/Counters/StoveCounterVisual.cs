using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject particlesObject;
    [SerializeField] private GameObject stoveOnObject;


    public void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool isFrying = e.stoveState == StoveCounter.StoveState.Frying || e.stoveState == StoveCounter.StoveState.Fried;
        particlesObject.SetActive(isFrying);
        stoveOnObject.SetActive(isFrying);
    }


}
