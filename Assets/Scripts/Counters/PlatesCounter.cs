using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    private float spawnPlateTimer;
    private int platesCount;
    private int platesMaxCount = 5;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= 4f) {
            spawnPlateTimer = 0;
            SpawnPlate();
        }
    }

    private void SpawnPlate() {
        if (platesCount >= platesMaxCount) return;
        platesCount++;
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player) {
        if (platesCount <= 0 || player.HasKitchenObject()) return;
        platesCount--;

        KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }
}
