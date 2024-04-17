using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private Player player;
    private float lastStepTime;

    [SerializeField] private float stepInterval = 0.5f;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Start() {
        lastStepTime = 0;
    }

    private void Update() {
        lastStepTime += Time.deltaTime;

        if (lastStepTime >= stepInterval) {
            lastStepTime = 0;
            if (player.isWalking) {
                PlayStepSound();
            }
        }
    }

    private void PlayStepSound() {
        SoundManager.Instance.PlayFootStepsSound(player.transform.position);
    }



}
