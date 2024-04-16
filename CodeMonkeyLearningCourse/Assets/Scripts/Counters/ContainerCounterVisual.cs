using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private readonly string OPEN_CLOSE;
    [SerializeField] private ContainerCounter containerCounter;

    public void Awake() {
        animator = GetComponent<Animator>();

    }

    public void Start() {
        containerCounter.OnPlayerGrabObject += PlayAnimation;
    }

    public void PlayAnimation(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
