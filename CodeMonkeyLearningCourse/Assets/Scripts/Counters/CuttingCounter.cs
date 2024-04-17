using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IHasProgress;

public class CuttingCounter : BaseCounter, IHasProgress { 

    [SerializeField] private CuttingBoardRecipe[] cuttingObjectTargets;
    private int cuttingCounter = 0;

    public event EventHandler<OnProgressChangeEventArgs> OnProgressChange;


    public event EventHandler OnPlayerCut;


    public override void Interact(Player player) {
        if (HasKitchenObject() && player.HasKitchenObject()) {
            return; // Do nothing if both player and counter have objects
        }

        if (!HasKitchenObject() && !player.HasKitchenObject()) {
            return; // Do nothing if both player and counter don't have objects
        }

        if (HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);

            cuttingCounter = 0;

            OnProgressChange?.Invoke(this, new OnProgressChangeEventArgs { progressNormalized = 0 });


        }
        else if (IsCuttingObjectSO(player.GetKitchenObject().GetKitchenObjectSO())) {
            player.GetKitchenObject().SetKitchenObjectParent(this);
        }


    }

    public override void InteractAlternate(Player player) {
        if (!HasKitchenObject()) {
            return;
        }
        KitchenObjectSO initial = kitchenObject.GetKitchenObjectSO();
        CuttingBoardRecipe result = GetRecipeSO(initial);

        if (result == null) {
            return;
        }

        cuttingCounter++;
        PlayerCut((float)cuttingCounter / result.cuttingCounterMax);

        if (cuttingCounter >= result.cuttingCounterMax) {
            kitchenObject.DestroySelf();
            KitchenObject.SpawnKitchenObject(result.resultKitchenObjectSO, this);
            cuttingCounter = 0;
        }

    }

    private void PlayerCut(float progress) {
        OnProgressChange?.Invoke(this, new OnProgressChangeEventArgs { progressNormalized = progress });
        OnPlayerCut?.Invoke(this, EventArgs.Empty);
    }

    private bool IsCuttingObjectSO(KitchenObjectSO kitchenObjectSO) {
        foreach (CuttingBoardRecipe target in cuttingObjectTargets) {
            if (target.initialKitchenObjectSO == kitchenObjectSO) {
                return true;
            }
        }

        return false;
    }

    private CuttingBoardRecipe GetRecipeSO(KitchenObjectSO initial) {
        foreach (CuttingBoardRecipe target in cuttingObjectTargets) {
            if (target.initialKitchenObjectSO == initial) {
                return target;
            }
        }

        return null;
    }
}
