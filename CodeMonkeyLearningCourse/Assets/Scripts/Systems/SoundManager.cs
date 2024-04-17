using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            throw new System.Exception("There can only be one SoundManager.");
        }
    }

    [SerializeField] private AudioRefSO audioRefs;
    private void Start() {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFail += DeliveryManager_OnDeliveryFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnPlayerCutObject;
        TrashCounter.OnAnyPlayerTrashObject += TrashCounter_OnAnyPlayerTrashObject;
        Player.Instance.OnPlayerGrabObject += Player_OnPlayerGrabObject;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        //Player.Instance.OnPlayerDropObject += Player_OnPlayerDropObject;
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, EventArgs e) {
        Vector3 position = ((BaseCounter)sender).transform.position;
        PlaySound(audioRefs.objectDrop, position);
    }


    // ********** Event Handlers **********

    //private void Player_OnPlayerDropObject(object sender, EventArgs e) {
    //    Vector3 position = Player.Instance.transform.position;
    //    PlaySound(audioRefs.objectDrop, position);
    //}

    private void Player_OnPlayerGrabObject(object sender, EventArgs e) {
        Vector3 position = Player.Instance.transform.position;
        PlaySound(audioRefs.objectPickup, position);
    }

    private void TrashCounter_OnAnyPlayerTrashObject(object sender, EventArgs e) {
        Vector3 position = ((TrashCounter)sender).transform.position;
        PlaySound(audioRefs.trash, position);
    }

    private void CuttingCounter_OnPlayerCutObject(object sender, EventArgs e) {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        Vector3 position = cuttingCounter.transform.position;
        PlaySound(audioRefs.chop, position);
    }

    private void DeliveryManager_OnDeliveryFail(object sender, EventArgs e) {
        Vector3 position = DeliveryCounter.Instance.transform.position;
        PlaySound(audioRefs.deliveryFailed, position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, EventArgs e) {
        Vector3 position = DeliveryCounter.Instance.transform.position;
        PlaySound(audioRefs.deliverySuccess, position);
    }


    // ********** END Event Handlers **********

    // ********** Helper Methods **********

    private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f) { 
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    private void PlaySound(AudioClip[] clips, Vector3 position, float volume = 1f) {
        AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        PlaySound(clip, position, volume);
    }

    public void PlayFootStepsSound(Vector3 position, float volume = 1f) { 
        PlaySound(audioRefs.footsteps, position, volume);  
    }

    // ********** END Helper Methods **********

}
