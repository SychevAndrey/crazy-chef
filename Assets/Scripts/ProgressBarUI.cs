using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private Image progressBar;
    [SerializeField] private CuttingCounter cuttingCounter;

    void LateUpdate() {
        transform.forward = Camera.main.transform.forward;
    }

    private void Start() {
        cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;
        progressBar.fillAmount = 0f;

        gameObject.SetActive(false);
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventArgs e) {
        progressBar.fillAmount = e.cuttingProgressNormalized;

        if (e.cuttingProgressNormalized == 0f || e.cuttingProgressNormalized == 1f) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
}
