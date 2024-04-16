using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {

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





}
