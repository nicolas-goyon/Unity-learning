using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSFX : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += PlayCookingSound;
    }

    private void PlayCookingSound(object sender, StoveCounter.OnStateChangedEventArgs e) {
        if (e.stoveState == StoveCounter.StoveState.Frying || e.stoveState == StoveCounter.StoveState.Fried) { 
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }
}
