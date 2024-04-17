using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            return;
        }

        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
    }

}
