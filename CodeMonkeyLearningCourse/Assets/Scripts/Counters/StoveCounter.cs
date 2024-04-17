using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    public enum StoveState {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private StoveState stoveState = StoveState.Idle;

    [SerializeField] private FryingRecipe[] fryingRecipe;
    [SerializeField] private BurningRecipe[] burningRecipe;

    private float fryingTimer = 0;
    private FryingRecipe currentFryingRecipe;

    private float burningTimer = 0;
    private BurningRecipe currentBurningRecipe;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    public class OnStateChangedEventArgs : EventArgs {
        public StoveState stoveState;
    }



    public void Update() {

        if (!HasKitchenObject()) { return; }


        switch (stoveState) {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                Frying();
                break;
            case StoveState.Fried:
                Fried();
                break;
            case StoveState.Burned:
                Burned();
                break;
        }
    }


    private void Frying() {
        if (currentFryingRecipe == null) {
            return;
        }
        if (!HasKitchenObject()) { return; }

        if (!IsFryingRecipe(kitchenObject.GetKitchenObjectSO())) {
            return;
        }

        fryingTimer += Time.deltaTime;
        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progressNormalized = fryingTimer / currentFryingRecipe.fryingSecondsMax });

        if (fryingTimer >= currentFryingRecipe.fryingSecondsMax) {
            fryingTimer = 0;
            kitchenObject.DestroySelf();
            // Create new kitchen object
            KitchenObject.SpawnKitchenObject(currentFryingRecipe.resultKitchenObjectSO, this);
            currentBurningRecipe = GetBurningRecipe(currentFryingRecipe.resultKitchenObjectSO);
            currentFryingRecipe = null;
            ChangeState(StoveState.Fried);
        }

    }

    private void Fried() {
        if (currentBurningRecipe == null) {
            return;
        }
        if (!HasKitchenObject()) { return; }

        if (!IsBurningRecipe(kitchenObject.GetKitchenObjectSO())) {
            return;
        }

        burningTimer += Time.deltaTime;
        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progressNormalized = burningTimer / currentBurningRecipe.burningSecondsMax });

        if (burningTimer >= currentBurningRecipe.burningSecondsMax) {
            burningTimer = 0;
            kitchenObject.DestroySelf();
            // Create new kitchen object
            KitchenObject.SpawnKitchenObject(currentBurningRecipe.resultKitchenObjectSO, this);

            ChangeState(StoveState.Burned);
        }

    }

    private void Burned() {

    }

    public override void Interact(Player player) {
        if (HasKitchenObject() && player.HasKitchenObject()) {// if both player and counter have objects
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate) ) {
                bool isAdded = plate.TryAddIngredient(kitchenObject.GetKitchenObjectSO());
                if (isAdded) { 
                    GetKitchenObject().DestroySelf();
                    ChangeState(StoveState.Idle);
                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progressNormalized = 0 });
                }
            }

            return;
        }

        if (!HasKitchenObject() && !player.HasKitchenObject()) { // if both player and counter don't have objects
            return;
        }

        if (!player.HasKitchenObject()) {
            // Picking up
            kitchenObject.SetKitchenObjectParent(player);
            ChangeState(StoveState.Idle);
            OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progressNormalized = 0 });
        } 
        else if (player.HasKitchenObject()) {
            // Putting down 
            if (IsFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO())) {
                KitchenObject kitchenObject = player.GetKitchenObject();
                kitchenObject.SetKitchenObjectParent(this);
                currentFryingRecipe = GetFryingRecipe(kitchenObject.GetKitchenObjectSO());
                ChangeState(StoveState.Frying);
            }
        }
    }

    public bool IsFryingRecipe(KitchenObjectSO kitchenObjectSO) {
        foreach (FryingRecipe recipe in fryingRecipe) {
            if (recipe.initialKitchenObjectSO == kitchenObjectSO) {
                return true;
            }
        }
        return false;
    }

    public FryingRecipe GetFryingRecipe(KitchenObjectSO kitchenObjectSO) {
        foreach (FryingRecipe recipe in fryingRecipe) {
            if (recipe.initialKitchenObjectSO == kitchenObjectSO) {
                return recipe;
            }
        }
        return null;
    }


    public bool IsBurningRecipe(KitchenObjectSO kitchenObjectSO) {
        foreach (BurningRecipe recipe in burningRecipe) {
            if (recipe.initialKitchenObjectSO == kitchenObjectSO) {
                return true;
            }
        }
        return false;
    }

    public BurningRecipe GetBurningRecipe(KitchenObjectSO kitchenObjectSO) {
        foreach (BurningRecipe recipe in burningRecipe) {
            if (recipe.initialKitchenObjectSO == kitchenObjectSO) {
                return recipe;
            }
        }
        return null;
    }

    private void ChangeState(StoveState stoveState) {
        this.stoveState = stoveState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { stoveState = stoveState });
        fryingTimer = 0;
        burningTimer = 0;
    }

}
