using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public static event EventHandler OnAnyPlayerTrashObject;

    public override void Interact(Player player) {
        if (player.HasKitchenObject()){ 
            player.GetKitchenObject().DestroySelf();
            OnAnyPlayerTrashObject?.Invoke(this, new EventArgs());
        }
    }
}
