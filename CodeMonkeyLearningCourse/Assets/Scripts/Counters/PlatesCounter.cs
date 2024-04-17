using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    [SerializeField] private float plateSpawnTime = 5f;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    public int MaxPlateCount { get; private set; } = 5;

    public event EventHandler<PlateCountChangedEventArgs> OnPlateCountChanged;

    public class PlateCountChangedEventArgs : EventArgs {
        public int plateCount;
    }


    private int plateCount = 0;
    private float plateTimer = 0;
    private void Update() {
        if (plateCount >= MaxPlateCount) return;

        plateTimer += Time.deltaTime;
        if (plateTimer >= plateSpawnTime) {
            plateTimer = 0;
            ChangePlateNumber(+1);
        }

    }

    public override void Interact(Player player) {
        if (plateCount > 0 && !player.HasKitchenObject()) { 
            ChangePlateNumber(-1);
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
        }
    }

    private void ChangePlateNumber(int amount) { 
        plateCount += amount;
        OnPlateCountChanged?.Invoke(this, new PlateCountChangedEventArgs { plateCount = plateCount });
    }
}
