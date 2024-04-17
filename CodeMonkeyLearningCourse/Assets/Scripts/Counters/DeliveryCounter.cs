using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            throw new System.Exception("There are multiple DeliveryCounter instances");
        }
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            plateKitchenObject.DestroySelf();
        }
    }
}
