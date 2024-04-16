using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum Mode {
        LookAt,
        LookAtInverted
    }

    [SerializeField] private Mode mode = Mode.LookAt;


    private void LateUpdate() {
        switch (mode) {
            case Mode.LookAt:
                LookAt();
                break;
            case Mode.LookAtInverted:
                LookAtInverted();
                break;
        }
    }

    private void LookAt() {
        transform.LookAt(Camera.main.transform);
    }

    private void LookAtInverted() {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
