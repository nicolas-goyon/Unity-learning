using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string CUTTING;
    [SerializeField] private CuttingCounter cuttingCounter;

    public void Awake() {
        animator = GetComponent<Animator>();

    }

    public void Start() {
        cuttingCounter.OnPlayerCut += PlayAnimation;
    }

    public void PlayAnimation(object sender, System.EventArgs e) {
        animator.SetTrigger(CUTTING);
    }
}
