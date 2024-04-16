using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public EventHandler OnPlayerGrabObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Interact(Player player) {
        Instantiate(kitchenObjectSO.prefab, counterTopPoint).GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
    }

}
