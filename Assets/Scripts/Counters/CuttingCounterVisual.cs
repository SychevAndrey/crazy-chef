using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        cuttingCounter.OnCutting += CuttingCounter_OnCutting;
    }

    private void CuttingCounter_OnCutting(object sender, EventArgs e) {
        animator.SetTrigger("Cut");
    }
}
