using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;

    private List<GameObject> platesVisualGameObjects;

    private void Awake() {
        platesVisualGameObjects = new List<GameObject>();
    }

    private void Start() {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        Transform plateVisual = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffset = platesVisualGameObjects.Count * 0.1f;
        plateVisual.localPosition = new Vector3(0, plateOffset, 0);

        platesVisualGameObjects.Add(plateVisual.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        if (platesVisualGameObjects.Count == 0) return;

        GameObject plateGameObject = platesVisualGameObjects[platesVisualGameObjects.Count - 1];
        platesVisualGameObjects.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}
