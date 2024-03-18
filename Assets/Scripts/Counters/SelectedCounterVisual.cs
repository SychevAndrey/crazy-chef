using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualsArray;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == baseCounter) {
            foreach (GameObject visual in visualsArray) {
                visual.SetActive(true);
            }
        } else {
            foreach (GameObject visual in visualsArray) {
                visual.SetActive(false);
            }
        }
    }
}
