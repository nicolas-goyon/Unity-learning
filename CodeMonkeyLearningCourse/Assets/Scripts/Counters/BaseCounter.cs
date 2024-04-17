using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent{
    public static event EventHandler OnAnyObjectPlaced;
    [SerializeField] protected Transform counterTopPoint;
    protected KitchenObject kitchenObject;


    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null) 
            OnAnyObjectPlaced?.Invoke(this, new EventArgs());
    }
    public abstract void Interact(Player player);

    public virtual void InteractAlternate(Player player) {}
}
