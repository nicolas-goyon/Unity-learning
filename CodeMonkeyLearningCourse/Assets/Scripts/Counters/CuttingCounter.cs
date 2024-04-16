using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [System.Serializable]
    public class CuttingObjectTarget {
        public KitchenObjectSO kitchenObjectSO;
        public KitchenObjectSO resultKitchenObjectSO;
    }

    [SerializeField] private List<CuttingObjectTarget> cuttingObjectTargets;


    public override void Interact(Player player) {
        if (HasKitchenObject() && player.HasKitchenObject()) {
            return; // Do nothing if both player and counter have objects
        }

        if (!HasKitchenObject() && !player.HasKitchenObject()) {
            return; // Do nothing if both player and counter don't have objects
        }

        if (HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);
        }
        else {
            // player has kitchen object
            player.GetKitchenObject().SetKitchenObjectParent(this);
        }


    }

    public override void InteractAlternate(Player player) {
        if (!HasKitchenObject()) {
            return;
        }

        if (!IsCuttingObjectSO(kitchenObject.GetKitchenObjectSO())) {
            return;
        }

        KitchenObjectSO initial = kitchenObject.GetKitchenObjectSO();
        KitchenObjectSO result = GetResultKitchenObjectSO(initial);

        GetKitchenObject().DestroySelf();

        KitchenObject.SpawnKitchenObject(result, this);

    }

    private bool IsCuttingObjectSO(KitchenObjectSO kitchenObjectSO) {
        foreach (CuttingObjectTarget target in cuttingObjectTargets) {
            if (target.kitchenObjectSO == kitchenObjectSO) {
                return true;
            }
        }

        return false;
    }

    private KitchenObjectSO GetResultKitchenObjectSO(KitchenObjectSO initial) {
        foreach (CuttingObjectTarget target in cuttingObjectTargets) {
            if (target.kitchenObjectSO == initial) {
                return target.resultKitchenObjectSO;
            }
        }

        return null;
    }
}
