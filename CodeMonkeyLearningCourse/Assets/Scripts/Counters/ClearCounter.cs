using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {


    public override void Interact(Player player) {
        if (HasKitchenObject() && player.HasKitchenObject()) {// if both player and counter have objects
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate)) {
                bool isAdded = plate.TryAddIngredient(kitchenObject.GetKitchenObjectSO());
                if (isAdded) { 
                    GetKitchenObject().DestroySelf(); 
                }
            }
            else if (GetKitchenObject().TryGetPlate(out plate)) {
                bool isAdded = plate.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO());
                if (isAdded) { 
                    player.GetKitchenObject().DestroySelf(); 
                }
            }
            return; 
        }

        if (!HasKitchenObject() && !player.HasKitchenObject()) { // if both player and counter don't have objects
            return; 
        }

        if (HasKitchenObject()) {
            kitchenObject.SetKitchenObjectParent(player);
        }
        else { 
            // player has kitchen object
            player.GetKitchenObject().SetKitchenObjectParent(this);
        }

        
    }





}
