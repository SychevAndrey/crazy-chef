using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject hasProgressGameObject;

    private IProgressBar hasProgress;

    void LateUpdate() {
        transform.forward = Camera.main.transform.forward;
    }

    private void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IProgressBar>();
        hasProgress.OnProgressChanged += IProgressBar_OnProgressChanged;
        progressBar.fillAmount = 0f;

        gameObject.SetActive(false);
    }

    private void IProgressBar_OnProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e) {
        progressBar.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
}
