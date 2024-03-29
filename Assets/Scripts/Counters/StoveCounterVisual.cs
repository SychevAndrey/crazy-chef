using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stove;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.onStateChangedEventArgs e) {
        bool showVisual = e.state == StoveCounter.State.Cooking || e.state == StoveCounter.State.Cooked;
        stove.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
